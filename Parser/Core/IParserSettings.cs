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

        int StartPageId { get; }

        int EndPageId { get; }

        Dictionary<string, string[]> QueryParams { get; }

        string GetLinkByPageId(int pageId);
    }
}
