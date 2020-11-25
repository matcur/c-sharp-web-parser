using System;
using System.Threading;
using AngleSharp.Html.Parser;

namespace Parser.Core
{
    class WebParser<T> where T : class
    {
        public event DataLoaded<T> OnDataLoaded = (T data) => { };

        private IParser<T> parser;
        private IParserSettings settings;
        private HtmlLoader htmlLoader;
        private HtmlParser htmlParser = new HtmlParser();
        private bool isActive = false;

        public WebParser(IParser<T> parser, IParserSettings settings)
        {
            this.parser = parser;
            this.settings = settings;
            htmlLoader = new HtmlLoader(settings);
        }

        public void Start()
        {
            isActive = true;
            Work();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Work()
        {
            int pageId = settings.StartPageId;
            for (; pageId <= settings.EndPageId; pageId++)
            {
                if (!isActive)
                    return;

                var htmlString = await htmlLoader.LoadByPageId(pageId);
                var htmlDom = await htmlParser.ParseDocumentAsync(htmlString, new CancellationToken());

                T result = parser.Parse(htmlDom);

                OnDataLoaded.Invoke(result);
            }

            isActive = false;
        }
    }
}
