using System;
using System.IO;
using SoftwareMonkeys.FileNodes;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.SetUp.Install;
using SoftwareMonkeys.csAnt.Versions;

namespace SoftwareMonkeys.csAnt.SetUpFromLocalConsole
{
    class MainClass
    {

        public static string PackageName = "csAnt";

        public static Version Version { get;set; }

        public static string Status = "";

        public static string SourcePath { get;set; }

        public static string DestinationPath { get;set; }
        
        public static bool Overwrite { get;set; }
        
        public static bool Clear { get;set; }

        public static bool Update { get;set; }

        public static bool Import { get;set; }

        public static string ImportPath = "https://github.com/SoftwareMonkeys/csAnt.git";

        // TODO: Remove if not needed
        public static string NugetSourcePath = "https://www.myget.org/F/softwaremonkeys/";
        
        // TODO: Remove if not needed
        public static string NugetPath = "http://nuget.org/nuget.exe";

        public static string CloneSource { get;set; }

        public static bool Clone { get;set; }

        public static bool IsHelp { get;set; }

        public static bool Direct { get;set; }

        public static void Main (string[] args)
        {
            ParseArguments(args);

            if (IsHelp) {
                Help();
            } else {
                Console.WriteLine ("");
                Console.WriteLine ("Setting up csAnt from local files...");
                Console.WriteLine ("");

                Console.WriteLine ("");
                Console.WriteLine ("Source dir:");
                Console.WriteLine (SourcePath);
                Console.WriteLine ("");

                Console.WriteLine ("Destination dir:");
                Console.WriteLine (DestinationPath);
                Console.WriteLine ("");

                Installer installer = null;

                // If it's a direct install then use the direct local installer
                if (Direct)
                {
                    installer = new DirectLocalInstaller(
                        SourcePath,
                        DestinationPath,
                        Overwrite
                    );
                }
                else
                {
                    installer = new LocalInstaller(
                        SourcePath,
                        DestinationPath,
                        PackageName,
                        Overwrite
                    );
                }

                installer.Version = Version;
                installer.Status = Status;

                installer.Clear = Clear;

                installer.Import = Import;
                installer.ImportPath = ImportPath;
                
                installer.Clone = Clone;
                installer.CloneSource = CloneSource;

                installer.Install();

                new IntroWriter().Write();
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
            // TODO: Finish help
            Console.WriteLine ("");
            Console.WriteLine ("Help");
            Console.WriteLine ("");
            Console.WriteLine ("Syntax:");
            Console.WriteLine ("");
            Console.WriteLine ("  SetUpFromLocal.exe -Source=(sourceProjectPath)"); 
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
            Console.WriteLine ("");
            Console.WriteLine ("  -c, -clone");
            Console.WriteLine ("      Whether to import text files (eg. scripts) via git, which handles changes, merges, and commits back to the source project.");
            Console.WriteLine ("");
        }
        
        static public void ParseArguments(string[] args)
        {
            var arguments = new Arguments(args);

            if (arguments.KeylessArguments.Length > 0)
                SourcePath = arguments.KeylessArguments[0];
            else
                SourcePath = DetectSourceDirectory ();

            if (arguments.ContainsAny("d", "dest", "destination"))
                DestinationPath = arguments["d", "dest", "destination"];
            else
                DestinationPath = Environment.CurrentDirectory;

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

            // Status name
            if (arguments.ContainsAny("status"))
                Status = arguments["status"];
            if (String.IsNullOrEmpty(Status))
                Status = GetStatusFromCurrentNode();

            // Nuget source
            if (arguments.ContainsAny("s", "source"))
                NugetSourcePath = arguments["s", "source"];

            // Nuget path
            if (arguments.ContainsAny("n", "nuget", "nugetpath"))
                NugetPath = arguments["n", "nuget", "nugetpath"];

            // Overwrite
            Overwrite = arguments.ContainsAny(
                "o",
                "overwrite"
            );

            // Clear
            Clear = arguments.ContainsAny(
                "clear"
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

            Direct = arguments.Contains("direct");
            
            // Import
            Import = arguments.ContainsAny("i", "import")
                && (arguments["i", "import"].ToLower() != false.ToString().ToLower()
                || arguments["i", "import"].ToLower() == true.ToString().ToLower());

            if (Import)
            {
                var path = arguments["i", "import"];
                if (!String.IsNullOrEmpty(path)
                    && path.ToLower() != true.ToString().ToLower()
                    && path.ToLower() != false.ToString().ToLower())
                    ImportPath = path;
            }

            // Clone
            Clone = arguments.ContainsAny("c", "clone")
                && (arguments["c", "clone"].ToLower() != false.ToString().ToLower()
                || arguments["c", "clone"].ToLower() == true.ToString().ToLower());

            if (Clone)
            {
                var path = arguments["c", "clone"];
                if (!String.IsNullOrEmpty(path)
                    && path.ToLower() != true.ToString().ToLower()
                    && path.ToLower() != false.ToString().ToLower())
                    CloneSource = path;
            }
        }
        
        static public string GetStatusFromCurrentNode()
        {
            // TODO: Move NodeManager to a property
            var nodeManager = new FileNodeManager();
            if (nodeManager.State.CurrentNode != null
                && nodeManager.State.CurrentNode.Properties.ContainsKey("Status"))
                return nodeManager.State.CurrentNode.Properties["Status"];

            return String.Empty;
        }
    }
}
