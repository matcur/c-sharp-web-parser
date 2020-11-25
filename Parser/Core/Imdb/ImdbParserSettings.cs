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
        public ImdbParserSettings(int startId, Dictionary<string, string[]> queryParams = null)
        {
            StartPageId = startId;
            QueryParams.Add("start", new[]{ startId.ToString() });

            if (queryParams != null)
                QueryParams.Merge<string, string[]>(queryParams);
        }

        public string Url { get; } = "https://www.imdb.com/search/title";

        public string Prefix
        {
            get
            {
                throw new Exception("Imdb has not prefix. We can get next page with help 'start' query param");
            }
        }

        public int StartPageId { get; }

        // Imdb allows parse only 50 items per page
        public int EndPageId
        {
            get
            {
                return StartPageId;
            }
        }

        public Dictionary<string, string[]> QueryParams { get; } = new Dictionary<string, string[]>();

        public string GetLinkByPageId(int pageId)
        {
            var link = $"{Url}/?";
            foreach (var param in QueryParams.Keys)
                link += $"{param}={string.Join(",", QueryParams[param])}&";

            return link;
        }
    }
}
