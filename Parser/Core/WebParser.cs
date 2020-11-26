using System;
using System.Threading;
using AngleSharp.Html.Parser;

namespace Parser.Core
{
    class WebParser<T> where T : class
    {
        public event DataLoaded<T> OnDataLoaded = (T data) => { };

        public bool IsActive { get; private set; } = true;
        private IParser<T> parser;
        private IParserSettings settings;
        private HtmlLoader htmlLoader;
        private HtmlParser htmlParser = new HtmlParser();

        public WebParser(IParser<T> parser, IParserSettings settings)
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

                T result = parser.Parse(htmlDom);

                OnDataLoaded.Invoke(result);
            }

            IsActive = false;
        }
    }
}
