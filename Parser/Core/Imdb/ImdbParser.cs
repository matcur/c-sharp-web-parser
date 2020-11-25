using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Parser.Core.Imdb
{
    class ImdbParser : IParser<Dictionary<string, string>>
    {
        public Dictionary<string, string> Parse(IHtmlDocument document)
        {
            var elements = document.QuerySelectorAll(".lister-item-header > a").OfType<IHtmlAnchorElement>();

            var result = new Dictionary<string, string>();
            foreach (var element in elements)
                result.Add(element.TextContent, "https://www.imdb.com/" + element.PathName);

            return result;
        }
    }
}
