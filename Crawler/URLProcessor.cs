using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    class URLProcessor
    {
        private string OriginURL = "";
        private static string FiveFiltersBase = "http://166.111.80.151:8088/makefulltextfeed.php?";
        public URLProcessor(string url)
        {
            this.OriginURL = url;
        }

        private static string EscapeURL(string url)
        {
            return Uri.EscapeDataString(url); 
        }

        private static string GenerateFiveFiltersURL(string escapedURL)
        {
            return FiveFiltersBase + "url=" + escapedURL + "&links=preserve&format=json&submit=Create+Feed";
        }

        private static string RequestFiveFilters(string url)
        {
            return Util.WebRetrieve(GenerateFiveFiltersURL(url));
        }

        private static string RequestOriginURL(string url)
        {
            return Util.WebRetrieve(url);
        }

        //private 
    }
}
