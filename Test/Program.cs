using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
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
            JsonParseTest();
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
