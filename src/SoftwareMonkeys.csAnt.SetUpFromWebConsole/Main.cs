using System;
using System.Collections.Generic;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.SetUp;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole
{
    class MainClass
    {
        public static string DestinationPath { get;set; }

        public static bool Overwrite { get;set; }

        public static bool Update { get;set; }

        public static bool Import { get;set; }

        public static string ImportPath = "https://git01.codeplex.com/csant/";

        public static Version Version = new Version(0,0,0,0);

        public static string NugetSourcePath = "https://www.myget.org/F/softwaremonkeys/";

        public static string NugetPath = "http://nuget.org/nuget.exe";

        public static string PackageName = "csAnt";

        public static bool IsHelp { get;set; }

        public static bool ShowIntro = true;

        public static void Main (string[] args)
        {
            ParseArguments(args);

            if (IsHelp)
                Help();
            else
            {
                Console.WriteLine ("");
                Console.WriteLine ("Setting up csAnt...");
                Console.WriteLine ("");

                
                Console.WriteLine("PackageName:" + PackageName);
                Console.WriteLine("");
                Console.WriteLine("Version:" + (Version == new Version(0,0,0,0) ? "[Latest]" : Version.ToString()));
                Console.WriteLine("");
                Console.WriteLine("Destination path:");
                Console.WriteLine(DestinationPath);
                Console.WriteLine("");
                Console.WriteLine("nuget source feed path:");
                Console.WriteLine(NugetSourcePath);
                Console.WriteLine("");
                Console.WriteLine("nuget exe path:");
                Console.WriteLine(NugetPath);
                Console.WriteLine("");
    
                var nugetRetriever = new InstallerNugetRetriever(
                    NugetSourcePath,
                    DestinationPath,
                    Version
                );

                if (!String.IsNullOrEmpty(NugetPath))
                    nugetRetriever.NugetPath = NugetPath;

                var fileManager = new InstallerFileManager();
    
                if (Update)
                {
                    var updater = new Updater(
                        nugetRetriever,
                        fileManager
                        );

                    updater.Import = Import;
                    updater.ImportPath = ImportPath;
                    updater.Update();
                }
                else
                {
                    var installer = new Installer(
                        nugetRetriever,
                        fileManager
                        );

                    installer.Import = Import;
                    installer.ImportPath = ImportPath;

                    installer.Install();
                }

                if (ShowIntro)
                    OutputIntro();
            }
        }

        static public void ParseArguments(string[] args)
        {
            var arguments = new Arguments(args);

            // Is help
            IsHelp = arguments.ContainsAny("h", "help", "man");

            // Destination
            DestinationPath = Environment.CurrentDirectory;
            if (arguments.ContainsAny("d", "destination"))
                DestinationPath = Path.GetFullPath(arguments["d", "destination"]);

            // Version
            if (arguments.ContainsAny("v", "version"))
                Version = Version.Parse(arguments["v", "version"]);


            // Package name
            if (arguments.ContainsAny("p", "pkg", "package"))
                PackageName = arguments["p", "pkg", "package"];


            // Show intro
            if (arguments.ContainsAny("intro"))
                ShowIntro = Convert.ToBoolean(arguments["intro"]);

            // Version
            if (arguments.ContainsAny("s", "source"))
                NugetSourcePath = arguments["s", "source"];


            // Version
            if (arguments.ContainsAny("n", "nuget", "nugetpath"))
                NugetPath = arguments["n", "nuget", "nugetpath"];


            // Overwrite
            Overwrite = arguments.ContainsAny(
                "o",
                "overwrite"
            );

            // Update
            Update = arguments.ContainsAny(
                "u",
                "update"
            );

            // Import
            Import = arguments.ContainsAny(
                "i",
                "import"
            );

            if (Import)
            {
                var path = arguments["i", "import"];
                if (!String.IsNullOrEmpty(path))
                    ImportPath = path;
            }
        }

        static public void OutputIntro()
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
            Console.WriteLine("");
            Console.WriteLine("csAnt SetUp Help");
            Console.WriteLine("");
            Console.WriteLine("Syntax:");
            Console.WriteLine("");
            Console.WriteLine("  mono csAnt-SetUp.exe [arguments]");
            Console.WriteLine("");
            Console.WriteLine("Arguments:");
            Console.WriteLine("");
            Console.WriteLine("  -p, -pkg, -package");
            Console.WriteLine("      The name of the csAnt package to install. Default is 'csAnt'.");
            Console.WriteLine("");
            Console.WriteLine("  -v, -version");
            Console.WriteLine("      The version to install. Default is the latest version.");
            Console.WriteLine("");
            Console.WriteLine("  -d, -destination");
            Console.WriteLine("      The destination folder to install the files to (absolute or relative).");
            Console.WriteLine("");
            Console.WriteLine("  -f, -feed, -feedpath");
            Console.WriteLine("      The location of the nuget feed.");
            Console.WriteLine("");
            Console.WriteLine("  -n, -nuget, -nugetpath");
            Console.WriteLine("      The location of the nuget.exe file. This will be downloaded if it's a http path, or copied if it's a local/network path. Default is http://nuget.org/nuget.exe");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("");
            Console.WriteLine("  -o, -overwrite");
            Console.WriteLine("      Tells the installer to force overwrite of files.");
            Console.WriteLine("");
            Console.WriteLine("  -u, -update");
            Console.WriteLine("      Performs an update instead of a standard instead.");
            Console.WriteLine("");
            Console.WriteLine("  -i, -import");
            Console.WriteLine("      Whether to import text files (eg. scripts) via git, which handles changes, merges, and commits back to the source project.");
            Console.WriteLine("");
            Console.WriteLine("  -intro");
            Console.WriteLine("      Whether or not to show the introduction text. Default is true.");
            Console.WriteLine("");
        }
    }
}
