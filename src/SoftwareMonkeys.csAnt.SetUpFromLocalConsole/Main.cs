using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.Versions;

namespace SoftwareMonkeys.csAnt.SetUpFromLocalConsole
{
    class MainClass
    {
        public static string SourcePath { get;set; }

        public static string DestinationPath { get;set; }
        
        public static bool Overwrite { get;set; }

        public static bool Update { get;set; }

        public static bool Import { get;set; }

        public static string ImportPath = "https://git01.codeplex.com/csant/";

        // TODO: Remove if not needed
        public static string NugetSourcePath = "https://www.myget.org/F/softwaremonkeys/";
        
        // TODO: Remove if not needed
        public static string NugetPath = "http://nuget.org/nuget.exe";
        
        public static string PackageName = "csAnt";

        public static bool IsHelp { get;set; }

        public static Version Version { get;set; }

        public static void Main (string[] args)
        {
            ParseArguments(args);

            if (IsHelp) {
                Help();
            } else {
                Console.WriteLine ("");
                Console.WriteLine ("Setting up csAnt from local files...");
                Console.WriteLine ("");

                DestinationPath = Environment.CurrentDirectory;

                Overwrite = false;

                PackageName = "csAnt";

                Console.WriteLine ("");
                Console.WriteLine ("Source dir:");
                Console.WriteLine (SourcePath);
                Console.WriteLine ("");

                Console.WriteLine ("Destination dir:");
                Console.WriteLine (DestinationPath);
                Console.WriteLine ("");

                var installer = new LocalInstaller(
                    SourcePath,
                    DestinationPath,
                    PackageName,
                    Overwrite
                );
                installer.Install();
            }
        }

        static string DetectSourceDirectory ()
        {
            var sourceDir = String.Empty;
            var path = Environment.CurrentDirectory;

            Console.WriteLine("Detecting source directory...");

            while (true) {
                path = Path.GetDirectoryName(path);

                if (path.IndexOf(Path.DirectorySeparatorChar) == -1)
                    break;

                var csAntPath = path
                    + Path.DirectorySeparatorChar
                    + "SoftwareMonkeys"
                    + Path.DirectorySeparatorChar
                    + "csAnt";

                if (Directory.Exists(csAntPath))
                {
                    sourceDir = csAntPath;
                    break;
                }
            }
            Console.WriteLine (sourceDir);

            return sourceDir;
        }

        static public void Help()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Help");
            Console.WriteLine ("");
            Console.WriteLine ("Syntax:");
            Console.WriteLine ("");
            Console.WriteLine ("  SetUpFromLocal.exe (sourceProjectPath) [(destinationProjectPath)] -l:../Projects/SourceProject/InstallList.txt [-o]");
            Console.WriteLine ("");
            Console.WriteLine ("Arguments:");
            Console.WriteLine ("");
            Console.WriteLine (" sourceProjectPath - The path to the source project (the location being installed from).");
            Console.WriteLine ("");
            Console.WriteLine (" destinationProjectPath - The path to the destination project (the location being installed to). (Optional)");
            Console.WriteLine ("");
            Console.WriteLine (" -l:[listFile]");
            Console.WriteLine ("     The path to a text file containing the list of files to install.");
            Console.WriteLine ("");
            Console.WriteLine (" -o");
            Console.WriteLine ("     A flag indicating whether to overwrite files if they already exist.");
        }
        
        static public void ParseArguments(string[] args)
        {

            if (args.Length > 0) {
                // Source directory
                SourcePath = args [0];
    
                // Destination directory
                if (args.Length > 1)
                    DestinationPath = Path.GetFullPath (args [1]);
            } else {
                SourcePath = DetectSourceDirectory ();
            }
    
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


            // TODO: Remove if not needed
            // Show intro
            //if (arguments.ContainsAny("intro"))
            //    ShowIntro = Convert.ToBoolean(arguments["intro"]);

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

    }
}
