using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Parser.Core;

namespace Parser.Pages
{
    class ParserPage
    {
        private WebParser webParser;

        public void Parse (IParserSettings parserSettings, string cssSelector, DataLoaded ShowMethod)
        {
            webParser = new WebParser(parserSettings);

            webParser.DataLoaded += ShowMethod;
            webParser.Start(cssSelector);
        }

        public void Abort(DataLoaded ShowMethod)
        {
            if (webParser != null)
            {
                webParser.Abort();
                webParser.DataLoaded -= ShowMethod;
            }
        }
    }
}
