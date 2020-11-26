using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Parser.Core.Metacritic
{
    class MetacriticParser : IParser
    {
        public IHtmlCollection<IElement> Parse(IHtmlDocument document)
        {
            return document.QuerySelectorAll(".clamp-summary-wrap > a.title > h3");
        }
    }
}
