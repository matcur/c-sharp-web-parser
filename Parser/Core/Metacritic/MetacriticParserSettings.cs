using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Metacritic
{
    class MetacriticParserSettings : IParserSettings
    {
        public MetacriticParserSettings(int startPageId, int endPageId)
        {
            CheckPageIds(startPageId, endPageId);

            StartPageId = startPageId;
            EndPageId = endPageId;
        }

        public string Url { get; } = "https://www.metacritic.com/browse/games/score/metascore/all";

        public string Prefix { get; } = "";

        public int StartPageId { get; private set; }

        public int EndPageId { get; private set; }

        public Dictionary<string, string[]> QueryParams => throw new NotImplementedException();

        public string GetLinkByPageId(int pageId)
        {
            return $"{Url}/?page={pageId}";
        }

        private void CheckPageIds(int startPageId, int endPageId)
        {
            if (startPageId < 1)
                throw new Exception($"Start page id must be more than 0, {startPageId} given");

            if (endPageId < 1)
                throw new Exception($"Start page id must be more than 0, {endPageId} given");

            if (startPageId > endPageId)
                throw new Exception($"Start page id can't be less than end page id");
        }
    }
}
