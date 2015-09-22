﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abot.Crawler;
using Abot.Poco;

namespace Crawler
{
    class Crawler
    {
        public Crawler ()
        {
            CrawlConfiguration crawlConfig = new CrawlConfiguration();
            crawlConfig.CrawlTimeoutSeconds = 100;
            crawlConfig.MaxConcurrentThreads = 5;
            crawlConfig.MaxPagesToCrawl = 1000;
            crawlConfig.UserAgentString = "abot v1.0 http://code.google.com/p/abot";

            PoliteWebCrawler crawler = new PoliteWebCrawler(crawlConfig);

            crawler.PageCrawlStartingAsync += crawler_processPageCrawlStarting;
            crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;

            CrawlResult result = crawler.Crawl(new Uri("http://code.google.com/p/abot"));
            if (result.ErrorOccurred)
                Console.WriteLine("Error: {0}", result.ErrorException.Message);
            else
                Console.WriteLine("S: {0}", result.RootUri.AbsoluteUri);
        }

        void crawler_processPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("About to crawl link {0} which wwas found on page {1}", pageToCrawl.Uri.AbsoluteUri,
                pageToCrawl.ParentUri.AbsoluteUri);
        }

        void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != System.Net.HttpStatusCode.OK)
                Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
            else
                Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);
        }
    }
}
