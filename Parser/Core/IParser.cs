using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Parser.Core
{
    interface IParser
    {
        IHtmlCollection<IElement> Parse(IHtmlDocument document);
    }
}
