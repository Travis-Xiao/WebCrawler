using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Model;
using HtmlAgilityPack;
using System.IO;

namespace Crawler
{
    class Crawler
    {
        string ProjectRoot = "../../../";
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
            DateTime startTime = DateTime.Now;
            HashSet<string> links = new HashSet<string>();
            var fetcher = new HtmlWeb();
            Random rand = new Random();
            List<string> failedURLs = new List<string>();
            Dictionary<string, Dictionary<DateTime, int>> stat = new Dictionary<string, Dictionary<DateTime, int>>();

            for (int k = 0; k < queries.Count(); k ++)
            {
                string q = queries[k];
                Dictionary<DateTime, int> c = new Dictionary<DateTime, int>();

                for (int j = 0; j < timePoints.Count(); j ++)
                {
                    int currentLinkCount = links.Count();
                    DateTime d = timePoints[j];
                    c.Add(d, 0);
                    for (int i = 0; i < searchEnginePolicy.MaxPageCount; i ++)
                    {
                        string searchURL = searchEnginePolicy.SearchEngine
                            + searchEnginePolicy.QueryName + "=" + q
                            + "&" + searchEnginePolicy.StartFrom(i)
                            + "&" + searchEnginePolicy.ConvertDatetimeRange(d)
                            + "&" + searchEnginePolicy.MiscQueryParas;

                        System.Threading.Thread.Sleep(100 + rand.Next() % 1000);
                        try
                        {
                            var doc = fetcher.Load(searchURL);
                            var resultSet = doc.DocumentNode.SelectNodes(searchEnginePolicy.RecordSelector);

                            int t = links.Count();
                            if (resultSet != null)
                            {
                                foreach (var node in resultSet)
                                {
                                    string link = node.GetAttributeValue("href", "");
                                    if (!searchEnginePolicy.IsValidURL(link)) continue;
                                    links.Add(link);
                                    Console.WriteLine(links.Count() + "\t" + link);
                                }
                            }
                            else
                            {
                                // Skip the query directly
                                i = searchEnginePolicy.MaxPageCount;
                            }
                            Console.WriteLine("{7}:\t{0}/{1}\t{2}/{3}\t{4}/{5}\t{6}",
                                k, queries.Count(), j, timePoints.Count(), i, searchEnginePolicy.MaxPageCount, links.Count() - t, q);
                        }
                        catch (Exception) {
                            failedURLs.Add(searchURL);
                        }
                    }
                    int newLinkCount = links.Count() - currentLinkCount;
                    if (newLinkCount == 0)
                    {
                        break;
                    }
                    c[d] = newLinkCount;
                }

                stat.Add(q, c);
            }

            StreamWriter logWriter = new StreamWriter(ProjectRoot + "Failed.txt");
            foreach (var line in failedURLs)
                logWriter.WriteLine(line);
            logWriter.Close();

            StreamWriter statWriter = new StreamWriter(ProjectRoot + "stat.csv");
            foreach (var d in timePoints)
            {
                statWriter.Write("," + d.ToShortDateString());
            }
            statWriter.WriteLine();
            foreach (var pair in stat)
            {
                statWriter.Write(pair.Key);
                statWriter.Write(",");
                foreach (var p in pair.Value)
                {
                    statWriter.Write(p.Value + ",");
                }
                statWriter.WriteLine();
            }
            statWriter.Close();

            Console.WriteLine("Time used: " + (DateTime.Now - startTime).TotalMinutes + "\t" + links.Count());
        }
    }
}
