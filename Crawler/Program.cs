using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Model;

namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] keywords = new string[]
            {
                "hello",
                "csharp"
            };

            Policy p = new GooglePolicy();
            Crawler c = new Crawler(p);
            c.PrepareQueries(keywords);
            c.start();
        }
    }
}
