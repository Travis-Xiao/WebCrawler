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
        private static string rootDir = Directory.GetCurrentDirectory() + "/../../../";
        private static string ResourceRoot = rootDir + "Crawler/Resource/";
        private string TLDFile = ResourceRoot+ "TLD.csv";
        private string UserAgentsFile = ResourceRoot + "user_agents";
        private List<string> tlds = new List<string>();
        private List<string> UserAgentStrings = new List<string>();
        public Loader()
        {
            Console.WriteLine(TLDFile);
            Load();
        }

        private void Load()
        {
            tlds = File.ReadAllLines(this.TLDFile).Where(x => !string.IsNullOrEmpty(x)).ToList();
            UserAgentStrings = File.ReadAllLines(this.UserAgentsFile).Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetUserAgents()
        {
            return UserAgentStrings;
        }

        public List<string> GetTLD()
        {
            return this.tlds;
        }
    }
}
