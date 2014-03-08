using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using System.Diagnostics;
using SoftwareMonkeys.csAnt.IO.Compression;

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

            var url = GetUrl ("csAnt-standard-release");

            Console.WriteLine (url);

            DownloadReleaseZip(url);

            ShowIntro();
        }

        static public void ShowIntro()
        {
            var prefix = "";
            if (Path.DirectorySeparatorChar == '/')
            {
                prefix = "sh csAnt.sh";
            }
            else
            {
                prefix = "csAnt.bat";
            }
         

            Console.WriteLine ("");
            Console.WriteLine ("You can now launch scripts...");

            Console.WriteLine ("Syntax:");
            Console.WriteLine ("  {0} [ScriptName]", prefix);
            Console.WriteLine ("");
            Console.WriteLine ("Example:");
            Console.WriteLine ("  {0} HelloWorld", prefix);

            Console.WriteLine ("");
            Console.WriteLine ("To create a new script...");
            Console.WriteLine ("");

            Console.WriteLine ("1) Call the 'NewScript' command:");
            Console.WriteLine ("  {0} NewScript [YourScriptName]", prefix);
            Console.WriteLine ("");

            Console.WriteLine ("2) Open your script at '/scripts/[YourScriptName].cs' to add your code, then save.");
            Console.WriteLine ("");

            Console.WriteLine ("3) Launch your script:");
            Console.WriteLine ("  {0} [YourScriptName]", prefix);
            Console.WriteLine ("");
        }

        static public void DownloadReleaseZip(string url)
        {
            var zipFile = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "csAnt.zip";

            DownloadUtility.Download(url, zipFile, true);

            var zipper = new FileZipper();

            var unzippedPath = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "_csAnt-Unzipped";

            zipper.Unzip(zipFile, unzippedPath, "/");

            InstallFilesFromRelease(unzippedPath);
        }

        static public void InstallFilesFromRelease(string unzippedPath)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Installing files:");

            foreach (var file in Directory.GetFiles (unzippedPath, "*", SearchOption.AllDirectories))
            {
                var toFile = file.Replace(unzippedPath, Environment.CurrentDirectory);

                if (!File.Exists (toFile))
                {
                    File.Copy(file, toFile);

                    Console.WriteLine (toFile.Replace(Environment.CurrentDirectory, ""));
                }
                else
                {
                    Console.WriteLine ("Skipped: " + toFile.Replace(Environment.CurrentDirectory, ""));
                }
            }
            Console.WriteLine ("");
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
