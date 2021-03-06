using System;
using System.Collections.Generic;
using System.IO;
using SoftwareMonkeys.FileNodes;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.SetUp.Install;
using SoftwareMonkeys.csAnt.SetUp.Install.Retrieve;
using SoftwareMonkeys.csAnt.SetUp.Install.Unpack;
using SoftwareMonkeys.csAnt.SetUp.Update;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole
{
    class MainClass
    {

        public static string PackageName = "csAnt";
        public static Version Version = new Version(0,0,0,0);
        public static string Status = String.Empty;

        public static string DestinationPath { get;set; }

        public static bool Overwrite { get;set; }

        public static bool Clear { get;set; }

        public static bool Update { get;set; }

        public static bool Import { get;set; }

        public static string ImportPath = "https://github.com/SoftwareMonkeys/csAnt.git";

        public static string NugetSourcePath = "https://www.myget.org/F/softwaremonkeys/";

        public static string NugetPath = "http://nuget.org/nuget.exe";

        public static string CloneSource = "https://github.com/SoftwareMonkeys/csAnt.git";

        public static bool Clone { get;set; }

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
                Console.WriteLine("  " + DestinationPath);
                Console.WriteLine("");
    
                var nugetRetriever = new InstallerNugetPackageRetriever(
                    NugetSourcePath,
                    DestinationPath
                );

                if (!String.IsNullOrEmpty(NugetPath))
                    nugetRetriever.NugetPath = NugetPath;

                var unpacker = new InstallUnpacker();
    
                if (Update)
                {
                    var updater = new Updater(
                        nugetRetriever,
                        unpacker
                        );

                    updater.PackageName = PackageName;
                    updater.Version = Version;
                    updater.Status = Status;

                    updater.Clear = Clear;
                    updater.Import = Import;
                    updater.ImportPath = ImportPath;
                    updater.Clone = Clone;
                    updater.CloneSource = CloneSource;

                    updater.Update();
                }
                else
                {
                    var installer = new Installer(
                        nugetRetriever,
                        unpacker
                        );

                    installer.PackageName = PackageName;
                    installer.Version = Version;
                    installer.Status = Status;

                    installer.Clear = Clear;
                    installer.Import = Import;
                    installer.ImportPath = ImportPath;
                    installer.Clone = Clone;
                    installer.CloneSource = CloneSource;

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
            
            // Package name
            if (arguments.ContainsAny("p", "pkg", "package"))
                PackageName = arguments["p", "pkg", "package"];

            // Version
            if (arguments.ContainsAny("v", "version"))
                Version = Version.Parse(arguments["v", "version"]);
            
            // Status
            if (arguments.ContainsAny("status"))
                Status = arguments["status"];
            if (String.IsNullOrEmpty(Status))
                Status = GetStatusFromCurrentNode();

            // Show intro
            if (arguments.ContainsAny("intro"))
                ShowIntro = Convert.ToBoolean(arguments["intro"]);

            // Source
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

        static public void OutputIntro()
        {
            new IntroWriter().Write();
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
            Console.WriteLine("  -status");
            Console.WriteLine("      The version to install. Default is the stable release.");
            Console.WriteLine("");
            Console.WriteLine("  -d, -destination");
            Console.WriteLine("      The destination folder to install the files to (absolute or relative).");
            Console.WriteLine("");
            Console.WriteLine("  -s, -source");
            Console.WriteLine("      The location of the source nuget feed.");
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
            Console.WriteLine("      Performs an update instead of a standard install.");
            Console.WriteLine("");
            Console.WriteLine("  -i, -import");
            Console.WriteLine("      Whether to import text files (eg. scripts) via git, which handles changes, merges, and commits back to the source project.");
            Console.WriteLine("");
            Console.WriteLine("  -c, -clone");
            Console.WriteLine("      Whether to clone the source git repository to the destination.");
            Console.WriteLine("");
            Console.WriteLine("  -intro");
            Console.WriteLine("      Whether or not to show the introduction text. Default is true.");
            Console.WriteLine("");
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
