using AngleSharp.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Collections
{
    class HtmlCollection<T> : IHtmlCollection<T> where T : class, IElement
    {
        public T this[int index] => items.GetElementById(index.ToString());

        public T this[string id] => items.GetElementById(id);

        public int Length => items.Count();

        private List<T> items = new List<T>();

        public HtmlCollection(T item)
        {
            items.Add(item);
        }

        public HtmlCollection(IEnumerable<T> enumerable)
        {
            items.AddRange(enumerable);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
