using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System;

namespace Parser.Core
{
    class HtmlLoader
    {
        private readonly IParserSettings settings;
        private readonly HttpClient htmlClient = new HttpClient();

        public HtmlLoader(IParserSettings settings)
        {
            this.settings = settings;
        }

        public async Task<string> LoadByPageId(int pageId)
        {
            var link = settings.GetLinkByPageId(pageId);
            Console.WriteLine(link);
            var responce = await htmlClient.GetAsync(link);

            if (responce != null && responce.StatusCode == HttpStatusCode.OK)
                return await responce.Content.ReadAsStringAsync();

            throw new HttpRequestException("Something is wrong");
        }
    }
}
