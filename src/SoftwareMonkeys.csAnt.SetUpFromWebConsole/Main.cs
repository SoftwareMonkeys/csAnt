using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using System.Diagnostics;
using SoftwareMonkeys.csAnt.IO.Compression;
using SoftwareMonkeys.csAnt.IO;
using System.Linq;
using SoftwareMonkeys.csAnt.External.Nuget;
using SoftwareMonkeys.csAnt.SetUp.Common;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            var arguments = new Arguments(args);

            if (arguments.ContainsAny("h", "help", "man"))
                Help();
            else
            {
                Console.WriteLine ("");
                Console.WriteLine ("Setting up csAnt...");
                Console.WriteLine ("");
    
                // TODO: Clean up and reorganise code
                var destinationPath = Environment.CurrentDirectory;
    
    
                if (arguments.ContainsAny("d", "destination"))
                    destinationPath = Path.GetFullPath(arguments["d", "destination"]);
    
                var overwrite = arguments.ContainsAny(
                    "o",
                    "overwrite"
                );
    
                var update = arguments.ContainsAny(
                    "u",
                    "update"
                );
    
                var version = new Version(0,0,0,0);
    
                if (arguments.ContainsAny("v", "version"))
                    version = Version.Parse(arguments["v", "version"]);
    
                var showIntro = true;
                if (arguments.ContainsAny("i", "intro"))
                    showIntro = Convert.ToBoolean(arguments["i", "intro"]);
    
                var releaseName = "standard";
    
                if (arguments.Contains ("r"))
                    releaseName = arguments["r"];
    
                if (releaseName.IndexOf("-release") > -1)
                    releaseName = releaseName.Replace("-release", "");
 
    
                if (update)
                    new Updater().Update(releaseName);
                else
                    new Installer().Install(releaseName, version, overwrite);
    
                if (showIntro)
                    ShowIntro();
            }
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

        static public void Help()
        {
        }
    }
}
