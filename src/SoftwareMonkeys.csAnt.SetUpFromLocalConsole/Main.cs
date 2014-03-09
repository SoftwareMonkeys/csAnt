using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp.Common;

namespace SoftwareMonkeys.csAnt.SetUpFromLocalConsole
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            var arguments = new Arguments (args);

            if (arguments.Contains ("h")
                || arguments.Contains ("help")) {
                Help();
            } else {
                Console.WriteLine ("");
                Console.WriteLine ("Setting up csAnt and the package manager from local files...");
                Console.WriteLine ("");

                var sourceDir = String.Empty;

                var destinationDir = String.Empty;

                var overwrite = false;

                var fileListFile = String.Empty;

                if (args.Length > 0) {
                    sourceDir = args [0];

                    destinationDir = Path.GetFullPath (args [1]);

                    overwrite = arguments.Contains ("o");

                    if (arguments.Contains ("l"))
                        fileListFile = arguments ["l"];

                } else {
                    sourceDir = DetectSourceDirectory ();

                    destinationDir = Environment.CurrentDirectory;
                }

                Console.WriteLine ("");
                Console.WriteLine ("Source dir:");
                Console.WriteLine (sourceDir);
                Console.WriteLine ("");

                Console.WriteLine ("Destination dir:");
                Console.WriteLine (destinationDir);
                Console.WriteLine ("");

                Console.WriteLine ("List file:");
                Console.WriteLine (fileListFile != String.Empty ? fileListFile : "[Not provided]");
                Console.WriteLine ("");

                var fileList = GetDefaultFileList ();
                if (!String.IsNullOrEmpty (fileListFile))
                    fileList = File.ReadAllLines (fileListFile);

                var installer = new LocalInstaller();
                installer.Install(
                    sourceDir,
                    destinationDir,
                    fileList,
                    overwrite
                    );
            }
        }

        static public string[] GetDefaultFileList()
        {
            return new string[]{
                "lib/csAnt/**",
                "lib/cs-script/**",
                "lib/FileNodes/**",
                "lib/HtmlAgilityPack/**",
                "scripts/*.cs",
                "csAnt.sh"
            };
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
    }
}
