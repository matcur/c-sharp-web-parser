using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Parser.Core.Imdb
{
    class ImdbParser : IParser
    {
        public IHtmlCollection<IElement> Parse(IHtmlDocument document)
        {
            return document.QuerySelectorAll(".lister-item-header > a");
        }
    }
}
