using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Core
{
    class WebParser
    {
        public event DataLoaded DataLoaded = delegate { };

        public event Action LoadFinished = delegate { };

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
            ParsePerPage(cssSelector);
        }

        public void Abort()
        {
            IsActive = false;
        }

        private async void ParsePerPage(string cssSelector)
        {
            int pageNumber = settings.StartPageNumber;
            for (; pageNumber <= settings.EndPageNumber; pageNumber++)
            {
                if (!IsActive)
                    return;

                var page = await LoadPage(pageNumber);
                DataLoaded.Invoke(domParser.Parse(page, cssSelector));
            }

            IsActive = false;
            LoadFinished.Invoke();
        }

        private async Task<IHtmlDocument> LoadPage(int pageNumber)
        {
            var htmlString = await htmlLoader.LoadByPageId(pageNumber);

            return await htmlParser.ParseDocumentAsync(htmlString, new CancellationToken());
        }
    }
}
