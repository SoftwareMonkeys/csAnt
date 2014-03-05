using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.cs
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Setting up csAnt from online files...");
            Console.WriteLine ("");

            var destinationPath = Environment.CurrentDirectory;

            var arguments = new Arguments(args);

            if (arguments.Contains ("d"))
                destinationPath = Path.GetFullPath(arguments["d"]);

            var overwrite = arguments.Contains("o");

            var list = GetFileList ();

            foreach (var key in list.Keys) {
                var file = destinationPath
                    + Path.DirectorySeparatorChar
                        + key;

                var url = list[key];

                DownloadUtility.Download(url, file, overwrite);
            }
        }

        static public Dictionary<string, string> GetFileList()
        {
            var list = new Dictionary<string, string>();

            // Launcher files
            list.Add ("/csAnt.bat", "https://csant.googlecode.com/git/csAnt.bat");
            list.Add ("/csAnt.sh", "https://csant.googlecode.com/git/csAnt.sh");

            // Repacked binary (contains all dependencies)
            list.Add ("/lib/csAnt/bin/Release/csAnt.exe", GetUrl("csAnt"));

            // Hello world script
            list.Add ("/scripts/HelloWorld.cs", "https://csant.googlecode.com/git/scripts/HelloWorld.cs");

            // TODO: Add other default scripts

            return list;
        }

        static public string GetUrl(string key)
        {
            var url = "https://code.google.com/p/csant/downloads/list";
    
            var xpath = "//table[@id='resultstable']/tr/td[3]";
    
            var prefix = "https://csant.googlecode.com/files/";
    
            var data = ScrapeXPathArray(
                url,
                xpath
            );
    
            foreach (string item in data)
            {
                if (item.IndexOf(key + "-") == 0)
                {    
                    return prefix + item;
                }
            }
    
            return String.Empty;
        }
        
        static public string[] ScrapeXPathArray(
            string url,
            string xpath
        )
        {
            var web = new HtmlWeb();
    
            var doc = web.Load(url);
    
            var nodes = doc.DocumentNode.SelectNodes(xpath);
    
            List<string> values = new List<string>();
    
            if (nodes != null)
            {    
                foreach (var node in nodes)
                {
                    if (!String.IsNullOrEmpty(node.InnerText.Trim ()))
                        values.Add (node.InnerText.Trim ());
                }
            }
    
            return values.ToArray();
        }
    }
}
