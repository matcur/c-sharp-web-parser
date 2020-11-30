using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Parser.Core
{
    class DomParser
    {
        public IHtmlCollection<IElement> Parse(IHtmlDocument document, string cssSelector)
        {
            return document.QuerySelectorAll(cssSelector);
        }

        public IElement ParseFirst(IHtmlDocument document, string cssSelector)
        {
            return document.QuerySelector(cssSelector);
        }

        public TElement ParseFirst<TElement>(IHtmlDocument document, string cssSelector) where TElement : class, IElement
        {
            return document.QuerySelector<TElement>(cssSelector);
        }
    }
}
