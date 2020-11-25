using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.ExtensionMethods
{
    public static class DictionaryExtensions
    {
        public static void Merge<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> mergeDictionary)
        {
            foreach (var key in mergeDictionary.Keys)
            {
                dictionary[key] = mergeDictionary[key];
            }
        }
    }
}
