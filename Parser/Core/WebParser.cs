using System;
using System.Threading;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace Parser.Core
{
    class WebParser
    {
        public event DataLoaded OnDataLoaded = (IHtmlCollection<IElement> data) => { };

        public bool IsActive { get; private set; } = true;
        private IParser parser;
        private IParserSettings settings;
        private HtmlLoader htmlLoader;
        private HtmlParser htmlParser = new HtmlParser();

        public WebParser(IParser parser, IParserSettings settings)
        {
            this.parser = parser;
            this.settings = settings;
            htmlLoader = new HtmlLoader(settings);
        }

        public void Start()
        {
            IsActive = true;
            Work();
        }

        public void Abort()
        {
            IsActive = false;
        }

        private async void Work()
        {
            int pageNumber = settings.StartPageNumber;
            for (; pageNumber <= settings.EndPageNumber; pageNumber++)
            {
                if (!IsActive)
                    return;

                var htmlString = await htmlLoader.LoadByPageId(pageNumber);
                var htmlDom = await htmlParser.ParseDocumentAsync(htmlString, new CancellationToken());

                var result = parser.Parse(htmlDom);
                OnDataLoaded.Invoke(result);
            }

            IsActive = false;
        }
    }
}
