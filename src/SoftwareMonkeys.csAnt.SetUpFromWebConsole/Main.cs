using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using System.Diagnostics;
using SoftwareMonkeys.csAnt.IO.Compression;
using SoftwareMonkeys.csAnt.IO;
using System.Linq;
using SoftwareMonkeys.csAnt.External.Nuget;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Setting up csAnt...");
            Console.WriteLine ("");

            // TODO: Clean up and reorganise code
            var destinationPath = Environment.CurrentDirectory;

            var arguments = new Arguments(args);

            if (arguments.Contains ("d"))
                destinationPath = Path.GetFullPath(arguments["d"]);

            var overwrite = arguments.Contains("o");
            
            var showIntro = true;
            if (arguments.Contains("i"))
                showIntro = Convert.ToBoolean(arguments["i"]);

            var releaseName = "standard";

            if (arguments.Contains ("r"))
                releaseName = arguments["r"];

            if (releaseName.IndexOf("-release") > -1)
                releaseName = releaseName.Replace("-release", "");

            Install(overwrite);

            if (showIntro)
                ShowIntro();
        }

        // TODO: Move this code into a dedicated installer component
        static public void Install(bool overwrite)
        {
            new Installer().Install(overwrite);
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
    }
}
