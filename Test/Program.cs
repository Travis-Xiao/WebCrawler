using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using JObject = Newtonsoft.Json.Linq.JObject;
using JToken = Newtonsoft.Json.Linq.JToken;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetRequestTest();
            //DatetimeTest();
            //string url = "https://code.google.com/p/abot/wiki/v1d5HomePage";
            //Console.WriteLine(Uri.EscapeUriString(url));
            //Console.WriteLine(Uri.EscapeDataString(url));
            //JsonParseTest();
            RegexTest();
        }

        static void RegexTest()
        {
            Regex r = new Regex(@"cache\.baidu\.com");
            string url = "http://cache.baidu.com/c?m=9f65cb4a8c8507ed4fece763105392230e54f73b6cd0d3027fa3cf1fd5790801013db2e5703f1102d8ce767001d8131ab5e4732f77552ff5d08ed21781ac92596eca796f36&amp;p=8b2a970e90934eaf5bebf839534194&amp;newp=c067f916d9c159ee07bd9b7e0e1089231610db2151d4d5156795cc&amp;user=baidu";
            Match m = r.Match(url);
            Console.WriteLine(m.Success);
        }

        static void JsonParseTest()
        {
            string url = "http://166.111.80.151:8088/makefulltextfeed.php?url=www.i-programmer.info%2Fnews%2F89-net%2F8749-aspnet-5-beta5-released.html&max=5&links=preserve&exc=&format=json&submit=Create+Feed";

            string content = Util.WebRetrieve(url);
            JToken token = JObject.Parse(content);
            var item = token.SelectToken("rss").SelectToken("channel").SelectToken("item");
            var title = (string) item.SelectToken("title");
            Console.WriteLine(title);
            var desc = (string) item.SelectToken("description");
            Console.WriteLine(desc);
        }

        static void DatetimeTest()
        {
            DateTime date = DateTime.Now;
            //Console.WriteLine(date.ToFileTime());
            //Console.WriteLine(date.ToFileTimeUtc());
            //Console.WriteLine(date.ToLocalTime());
            //Console.WriteLine(date.ToLongDateString());
            //Console.WriteLine(date.ToLongTimeString());
            //Console.WriteLine(date.ToOADate());
            //Console.WriteLine(date.ToShortDateString());
            //Console.WriteLine(date.ToShortTimeString());
            //Console.WriteLine(date.ToString());
            //Console.WriteLine(date.ToUniversalTime());
            //Console.WriteLine(date.ToShortDateString());
        }

        static void GetRequestTest()
        {
            string url = "http://www.baidu.com";
            WebRequest req = WebRequest.Create(url);
            Stream obj = req.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(obj);

            string line = "";
            int i = 0;

            while (line != null)
            {
                i++;
                line = reader.ReadLine();
                reader.ReadToEnd();
                if (line != null) Console.WriteLine("{0}:{1}", i, line);
            }
        }

        static void DateTest()
        {
            DateTime date = new DateTime(2015, 9, 22);
            Console.WriteLine(Util.ConvertToUnixTimestamp(date));
            Console.WriteLine(Util.ConvertToUnixTimestamp(new DateTime(2015, 9, 23)));
        }
    }
}
