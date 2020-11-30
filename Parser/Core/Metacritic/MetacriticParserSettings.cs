using System;
using System.Collections.Generic;

namespace Parser.Core.Metacritic
{
    class MetacriticParserSettings : IParserSettings
    {
        public MetacriticParserSettings(int startPageNumber, int endPageNumber)
        {
            CheckPageIds(startPageNumber, endPageNumber);

            StartPageNumber = startPageNumber;
            EndPageNumber = endPageNumber;
        }

        public string Url { get; } = "https://www.metacritic.com/browse/games/score/metascore/all";

        public string Prefix { get; } = "";

        public int StartPageNumber { get; private set; }

        public int EndPageNumber { get; private set; }

        public Dictionary<string, string[]> QueryParams { get; private set; } = new Dictionary<string, string[]>();

        public string GetLinkByPageNumber(int pageNumber)
        {
            // Вычетаю 1 т.к отчет на metacritic начинаеься с 0
            return $"{Url}/?page={pageNumber - 1}";
        }

        private void CheckPageIds(int startPageNumber, int endPageNumber)
        {
            if (startPageNumber < 1)
                throw new Exception($"Start page id must be more than 0, {startPageNumber} given");

            if (endPageNumber < 1)
                throw new Exception($"Start page id must be more than 0, {endPageNumber} given");

            if (startPageNumber > endPageNumber)
                throw new Exception($"Start page id can't be less than end page id");
        }
    }
}
