using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler.Model
{
    class GooglePolicy : Policy
    {
        string  SearchEngineBase = "https://www.google.com.hk/search?";
        int     RecordPerPage = 100;
        public  int MaxRecordPerQuery { get { return 1000; } }
        string  Policy.SearchEngine { get { return SearchEngineBase; } }
        string  Policy.QueryName { get { return "q"; } }
        int     Policy.MaxPageCount { get { return (int) Math.Ceiling(MaxRecordPerQuery * 1.0 / RecordPerPage); } }

        string  Policy.MiscQueryParas { get {
            return @"lr=lang_en"
                + "&hl=en"
                + "&num=" + RecordPerPage
                + "&filter=1"
                + "&save=active"
            ;
        } }

        string  Policy.ConvertDatetimeRange(DateTime date)
        {
            DateTime endTime = date.AddDays(1);
            return "tbs=cdr:1,cd_min:" + date.ToString("dd/MM/yyyy") + ",cd_max:" + endTime.ToString("dd/MM/yyyy");
        }

        string  Policy.StartFrom(int page)
        {
            return "start=" + Math.Max(0, page) * RecordPerPage;
        }
        int     Policy.GetPageRecord() { return RecordPerPage; }

        bool    Policy.IsValidURL(string url)
        {
            return true;
        }

        string  Policy.RecordSelector { get { return "//div[@class='g']//h3[@class='r']//a"; } }

        string  Policy.ParseRawURL(string url)
        {
            return Util.GetParameterFromURL(url, "q");
        }
    }
}
