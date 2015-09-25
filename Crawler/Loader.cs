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
        private static string rootDir = Directory.GetCurrentDirectory() + "../../../";
        private string TLDFile = rootDir + "Resource/TLD.csv";
        private List<string> tlds = new List<string>();
        public Loader()
        {
            Console.WriteLine(TLDFile);
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
