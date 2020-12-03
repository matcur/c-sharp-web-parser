using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.ExtensionMethods;

namespace Parser.Core.Imdb
{
    class ImdbParserSettings : IParserSettings
    {
        public string Url { get; } = "https://www.imdb.com/search/title";

        public string Prefix
        {
            get
            {
                throw new Exception("Imdb has not prefix. We can get next page with help 'start' query param");
            }
        }

        public int StartPageNumber { get; set; }

        // Imdb allows parse only 50 items per page
        public int EndPageNumber
        {
            get
            {
                return StartPageNumber;
            }
        }

        public ImdbParserSettings(int startNumber, Dictionary<string, string[]> queryParams = null)
        {
            StartPageNumber = startNumber;
            QueryParams.Add("start", new[] { startNumber.ToString() });

            if (queryParams != null)
                QueryParams.Merge<string, string[]>(queryParams);
        }

        public Dictionary<string, string[]> QueryParams { get; } = new Dictionary<string, string[]>
        {
            { "title_type", new []{ "tv_series", "tv_miniseries "} },
        };

        public string GetLinkByPageNumber(int pageNumber)
        {
            var link = $"{Url}/?";
            foreach (var param in QueryParams.Keys)
                link += $"{param}={string.Join(",", QueryParams[param])}&";

            return link;
        }
    }
}
