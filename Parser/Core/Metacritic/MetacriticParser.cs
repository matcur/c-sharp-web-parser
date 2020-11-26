using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace Parser.Core.Metacritic
{
    class MetacriticParser : IParser<List<string>>
    {
        public List<string> Parse(IHtmlDocument document)
        {
            var elements = document.QuerySelectorAll(".clamp-summary-wrap > a.title > h3");

            var games = new List<string>();
            foreach (var element in elements)
                games.Add(element.TextContent);

            return games;
        }
    }
}
