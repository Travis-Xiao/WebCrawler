using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JObject = Newtonsoft.Json.Linq.JObject;
using JToken = Newtonsoft.Json.Linq.JToken;

namespace Crawler
{
    class FiveFilters
    {
        private string FiveFiltersBase = "http://127.0.0.1:8088/makefulltextfeed.php?links=preserve&exc=&format=json&submit=Create+Feed&url=";
        public FiveFilters(string ffBase)
        {
            FiveFiltersBase = ffBase;
        }

        public Dictionary<string, string> PostProcess(string url)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            string content = Util.WebRetrieve(url);
            JToken token = JObject.Parse(content);
            var item = token.SelectToken("rss").SelectToken("channel").SelectToken("item");
            var title = (string)item.SelectToken("title");
            res.Add("title", title);
            var desc = (string)item.SelectToken("description");
            res.Add("description", desc);
            return res;
        }
    }
}
