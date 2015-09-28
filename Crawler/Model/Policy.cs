using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Crawler.Model
{
    interface Policy
    {
        string  ConvertDatetimeRange     (DateTime date);
        string  SearchEngine        { get; }
        string  QueryName           { get; }
        string  StartFrom           (int page);
        int     GetPageRecord       ();
        string  MiscQueryParas      { get; }
        int     MaxRecordPerQuery   { get; }
        int     MaxPageCount        { get; }
        string  RecordSelector      { get; }
        bool    IsValidURL(string url);
        string  ParseRawURL(string url);
    }
}
