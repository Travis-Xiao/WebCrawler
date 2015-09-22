using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Crawler
{
    class Loader
    {
        private string TLDFile = "TLD.csv";
        private List<string> tlds = new List<string>();
        public Loader()
        {
            Load();
        }

        private void Load()
        {
            tlds = File.ReadAllLines(this.TLDFile).Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetTLD()
        {
            return this.tlds;
        }
    }
}
