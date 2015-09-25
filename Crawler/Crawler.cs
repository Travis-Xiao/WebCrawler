using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Model;
using HtmlAgilityPack;

namespace Crawler
{
    class Crawler
    {
        private string[] GoogleAlternatives =
        {
            "https://duckduckgo.com",

        };
        private List<string> queries;
        private List<DateTime> timePoints;
        private DateTime EarliestTime = DateTime.Now.AddYears(-5);
        private Loader loader = new Loader();
        private Policy searchEnginePolicy;

        public Crawler(Policy p)
        {
            searchEnginePolicy = p;
            queries = new List<string>();
            timePoints = new List<DateTime>();
        }

        public void PrepareQueries(string [] keywords)
        {
            PrepareQueries(new List<string>(keywords));
        }

        public void PrepareQueries(List<string> keywords)
        {
            string qBase = string.Join("+", keywords);
            // combine with various TLDs
            queries.Add(qBase);
            qBase += (" site: *");
            foreach (var tld in loader.GetTLD())
            {
                queries.Add(qBase + tld);
            }

            // combine with various time points with 1d interval
            DateTime queryEnd = DateTime.Now;
            TimeSpan span = new TimeSpan(24, 0, 0);
            while (EarliestTime < queryEnd)
            {
                timePoints.Add(queryEnd - span);
                queryEnd = queryEnd.AddDays(-1);
            }
        }

        public void start()
        {
            
            HashSet<string> links = new HashSet<string>();
            var fetcher = new HtmlWeb();
            foreach (DateTime d in timePoints)
            {
                foreach (string q in queries)
                {
                    for (int i = 0; i < searchEnginePolicy.MaxPageCount; i ++)
                    {
                        string searchURL = searchEnginePolicy.SearchEngine
                            + searchEnginePolicy.QueryName + "=" + q
                            + "&" + searchEnginePolicy.StartFrom(i)
                            + "&" + searchEnginePolicy.ConvertDatetimeRange(d)
                            + "&" + searchEnginePolicy.MiscQueryParas;

                        var doc = fetcher.Load(searchURL);
                        var resultSet = doc.DocumentNode.SelectNodes(searchEnginePolicy.RecordSelector);
                        if (resultSet == null) continue;
                        foreach (var node in resultSet)
                        {
                            string link = node.GetAttributeValue("href", "");
                            links.Add(link);
                            Console.WriteLine(link);
                        }
                    }
                }
            }
            Console.WriteLine(links.Count());
        }
    }
}
