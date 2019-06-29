using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GitHubAPITests.RestHelpers
{
   public class JSONUtils
    {
        public JObject GetJSONObjectFromFile(string api)
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Environment.CurrentDirectory = Directory.GetDirectories("../../Resources")[0];
            string pathToJsonFile = "";
            switch (api)
            {
                case "createIssue":
                    pathToJsonFile = Directory.GetCurrentDirectory() + "\\CreateIssue.json";
                    break;
                case "EditIssue":
                    pathToJsonFile = Directory.GetCurrentDirectory() + "\\EditIssue.json";
                    break;               
                default:
                    break;
            }
            return JObject.Parse(File.ReadAllText(pathToJsonFile));
        }
    }
}
