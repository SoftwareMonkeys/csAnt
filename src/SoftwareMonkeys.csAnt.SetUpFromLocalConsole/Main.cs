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
        public static void Main (string[] args)
        {
            var arguments = new Arguments (args);

            if (arguments.ContainsAny ("h", "help", "man")) {
                Help();
            } else {
                Console.WriteLine ("");
                Console.WriteLine ("Setting up csAnt and the package manager from local files...");
                Console.WriteLine ("");

                var sourceDir = String.Empty;

                var destinationDir = Environment.CurrentDirectory;

                var overwrite = false;

                var filePatternsFile = String.Empty;

                var packageName = "csAnt";

                if (args.Length > 0) {
                    // Source directory
                    sourceDir = args [0];

                    // Destination directory
                    if (args.Length > 1)
                        destinationDir = Path.GetFullPath (args [1]);

                    overwrite = arguments.Contains ("o");

                    if (arguments.Contains ("l"))
                        filePatternsFile = arguments ["l"];

                    if (arguments.Contains ("p"))
                        packageName = arguments ["p"];

                } else {
                    sourceDir = DetectSourceDirectory ();
                }

                Console.WriteLine ("");
                Console.WriteLine ("Source dir:");
                Console.WriteLine (sourceDir);
                Console.WriteLine ("");

                Console.WriteLine ("Destination dir:");
                Console.WriteLine (destinationDir);
                Console.WriteLine ("");

                Console.WriteLine ("File patterns file:");
                Console.WriteLine (filePatternsFile != String.Empty ? filePatternsFile : "[Not provided]");
                Console.WriteLine ("");

                // TODO: Shorten code
                if (!String.IsNullOrEmpty(filePatternsFile))
                {

                    var installer = new LocalInstaller(
                        sourceDir,
                        destinationDir,
                        packageName,
                        filePatternsFile,
                        overwrite
                    );
                    installer.Install();
                }
                else
                {
                    var installer = new LocalInstaller(
                        sourceDir,
                        destinationDir,
                        packageName,
                        overwrite
                    );
                    installer.Install();
                }
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
    }
}
