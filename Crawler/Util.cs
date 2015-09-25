using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using JObject = Newtonsoft.Json.Linq.JObject;
using JToken = Newtonsoft.Json.Linq.JToken;

namespace Crawler
{
    class Util
    {
        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static string WebRetrieve(string url)
        {
            WebRequest req = WebRequest.Create(url);
            Stream obj = req.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(obj);

            return reader.ReadToEnd() + "";
        }

        public static Dictionary<string, string> ParseJson(string json)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            JToken token = JObject.Parse(json);
            var item = token.SelectToken("rss").SelectToken("channel").SelectToken("item");
            var title = (string) item.SelectToken("title");
            res.Add("title", title);
            var desc = (string) item.SelectToken("description");
            res.Add("description", desc);
            return res;
        }
    }
}
