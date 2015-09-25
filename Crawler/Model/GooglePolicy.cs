using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Model
{
    class GooglePolicy : Policy
    {
        string  SearchEngineBase = "http://www.google.com/search?";
        int     RecordPerPage = 100;
        int     Policy.MaxRecordPerQuery { get { return 1000; } }
        string  Policy.SearchEngine { get { return SearchEngineBase; } }
        public  int MaxRecordPerQuery { get { return 1000; } }
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
            return "tbs=cdr:1,cd_min:" + date.ToShortDateString() + ",cd_max:" + endTime.ToShortDateString();
        }

        string  Policy.StartFrom(int page)
        {
            return "start=" + Math.Max(0, page) * RecordPerPage;
        }
        string  RecordSelector { get { return ""; } }
        int     Policy.GetPageRecord() { return RecordPerPage; }
        string  Policy.RecordSelector { get { throw new NotImplementedException(); } }
    }
}
