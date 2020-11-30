using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Parser.Core.Collections;

namespace Parser.Core
{
    class WebParser
    {
        public event DataLoaded OnDataLoaded = (IHtmlCollection<IElement> data) => { };
        public bool IsActive { get; private set; } = true;

        private IParserSettings settings;
        private DomParser domParser = new DomParser();
        private HtmlLoader htmlLoader;
        private HtmlParser htmlParser = new HtmlParser();

        public WebParser(IParserSettings settings)
        {
            this.settings = settings;
            htmlLoader = new HtmlLoader(settings);
        }

        public void Start(string cssSelector)
        {
            IsActive = true;
            Parse(cssSelector);
        }

        public void StartParseFirst(string cssSelector)
        {
            IsActive = true;
            ParseFirst(cssSelector);
        }

        public void Abort()
        {
            IsActive = false;
        }

        private async void Parse(string cssSelector)
        {
            int pageNumber = settings.StartPageNumber;
            for (; pageNumber <= settings.EndPageNumber; pageNumber++)
            {
                if (!IsActive)
                    return;

                var page = await LoadPage(pageNumber);

                var result = domParser.Parse(page, cssSelector);
                OnDataLoaded.Invoke(result);
            }

            IsActive = false;
        }

        private async void ParseFirst(string cssSelector)
        {
            int pageNumber = settings.StartPageNumber;
            for (; pageNumber <= settings.EndPageNumber; pageNumber++)
            {
                if (!IsActive)
                    return;

                var page = await LoadPage(pageNumber);

                var element = domParser.ParseFirst(page, cssSelector);
                if (element == null)
                    continue;

                var htmlCollection = new HtmlCollection<IElement>(element);
                OnDataLoaded.Invoke(htmlCollection);
                break;
            }

            IsActive = false;
        }

        private async Task<IHtmlDocument> LoadPage(int pageNumber)
        {
            var htmlString = await htmlLoader.LoadByPageId(pageNumber);

            return await htmlParser.ParseDocumentAsync(htmlString, new CancellationToken());
        }
    }
}
