using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace Parser.Core
{
    interface IParser<out T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
