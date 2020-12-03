using AngleSharp.Html.Dom;
using Parser.Commands;
using Parser.Core;
using Parser.Core.Imdb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.ViewModels
{
    class ImdbViewModel : ViewModel
    {
        #region Properties
        public ObservableCollection<string> FoundFilms { get; private set; } = new ObservableCollection<string>();

        public ReplyCommand ParseCommand { get; private set; }

        public ReplyCommand AbortParseCommand { get; private set; }

        public ReplyCommand RemoveFilmsParseCommand { get; private set; }

        public int StartPageNumber
        {
            get { return startPageNumber; }
            set
            {
                startPageNumber = value;
                OnPropertyChange("StartPageNumber");
            }
        }

        public int EndPageNumber
        {
            get { return endPageNumber; }
            set
            {
                endPageNumber = value;
                OnPropertyChange("EndPageNumber");
            }
        }

        public bool CanStartParse
        {
            get
            {
                return !webParser.IsActive;
            }
        }
        #endregion

        private WebParser webParser;

        private ImdbParserSettings parserSettings;

        private int startPageNumber;

        private int endPageNumber;

        public ImdbViewModel()
        {

            InitCommands();

            FoundFilms.CollectionChanged += delegate
            {
                OnPropertyChange("FoundFilms");
            };
        }

        private void Parse(object o)
        {
            parserSettings = new ImdbParserSettings(StartPageNumber);
            webParser = new WebParser(parserSettings);

            SubscribeParseEvents();

            webParser.Start(".lister-item-header > a");
        }

        private void Abort(object o)
        {
            webParser.Abort();
            OnPropertyChange("CanStartParse");
        }

        private void RemoveFilms(object o)
        {
            FoundFilms.Clear();
        }

        private void InitCommands()
        {
            ParseCommand = new ReplyCommand(Parse);
            AbortParseCommand = new ReplyCommand(Abort);
            RemoveFilmsParseCommand = new ReplyCommand(RemoveFilms);
        }

        private void SubscribeParseEvents()
        {
            webParser.LoadFinished += delegate
            {
                OnPropertyChange("CanStartParse");
            };
            webParser.DataLoaded += loadedItems =>
            {
                var aTags = loadedItems.OfType<IHtmlAnchorElement>();
                foreach (var tag in aTags)
                    FoundFilms.Add($"{tag.TextContent} {tag.Href}");
            };
        }
    }
}
