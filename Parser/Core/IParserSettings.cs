using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core
{
    interface IParserSettings
    {
        string Url { get; }

        string Prefix { get; }

        int StartPageNumber { get; }

        int EndPageNumber { get; }

        Dictionary<string, string[]> QueryParams { get; }

        string GetLinkByPageNumber(int pageNumber);
    }
}
