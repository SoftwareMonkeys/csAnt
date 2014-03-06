using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using System.Diagnostics;

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

            GetDependencies();
        }

        static public void GetDependencies()
        {

            var libs = new string[]{
            //    "SharpZipLib", // TODO: Check if needed (shouldn't be at the moment because they're embedded in csAnt.exe)
            //    "Newtonsoft.Json",
            //    "CSScriptLibrary.dll"
            };

            Environment.CurrentDirectory = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "lib";

            if (libs.Length > 0)
            {
                Console.WriteLine ("");
                Console.WriteLine ("Getting dependencies...");
                Console.WriteLine ("");

                foreach (var lib in libs)
                {
                    Console.WriteLine (lib);
                    Process.Start("nuget.exe", "install " + lib);
                    Console.WriteLine ("Done");
                    
                }
            }

            Environment.CurrentDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);
        }

        static public Dictionary<string, string> GetFileList()
        {
            var list = new Dictionary<string, string>();

            // Launcher files
            list.Add ("/csAnt.bat", "https://csant.googlecode.com/git/csAnt.bat");
            list.Add ("/csAnt.sh", "https://csant.googlecode.com/git/csAnt.sh");

            // TODO: Check if needed. Isn't currently, but could be useful to be included.
            //list.Add ("/lib/nuget.exe", "http://nuget.org/nuget.exe");

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
