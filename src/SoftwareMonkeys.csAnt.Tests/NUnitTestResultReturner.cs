using System;
using System.IO;
using System.Text;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class NUnitTestResultReturner
    {
        public IScript Script { get;set; }

        public NUnitTestResultReturner (
            IScript script
        )
        {
            Script = script;
        }

        public void ReturnResults ()
        {
            if (Script.IsVerbose) {
                Console.WriteLine("");
                Console.WriteLine("Returning test results (from temporary directory)...");
                Console.WriteLine("Current directory:");
                Console.WriteLine(Script.CurrentDirectory);
                Console.WriteLine("");
            }

            // Return the results to the parent script directory
            if (Script.ParentScript != null)
                ReturnResultsToParent();

            // Return the results to the original directory
            ReturnResultsToOriginal();
        }

        public void ReturnResultsToParent ()
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Has a parent script. Returning to parent directory.");
                Console.WriteLine ("Parent directory:");
                Console.WriteLine (Script.ParentScript.CurrentDirectory);
                Console.WriteLine ("");
            }

            var parentDir = Script.ParentScript.CurrentDirectory;

            var currentDir = Script.CurrentDirectory;

            if (parentDir != currentDir) {
                CopyTestResults (
                currentDir,
                parentDir
                );
            } else {
                if (Script.IsVerbose)
                {
                    Console.WriteLine ("");
                    Console.WriteLine ("Current directory is same as parent directory. Skipping copy.");
                    Console.WriteLine ("");
                }
            }
        }

        public void ReturnResultsToOriginal ()
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Doesn't have a parent script. Returning to original directory.");
                Console.WriteLine ("Original directory:");
                Console.WriteLine (Script.OriginalDirectory);
                Console.WriteLine ("");
            }
            var originalDir = Script.OriginalDirectory;

            var currentDir = Script.CurrentDirectory;

            Script.AddSummary("Returned test results to: " + originalDir);

            // If the current directory is not the original directory
            if (originalDir != currentDir) {
                CopyTestResults (
                    currentDir,
                    originalDir
                );
            } else {
                if (Script.IsVerbose)
                    Console.WriteLine ("The current directory is the same as the original directory. Skipping copy.");
            }
        }
        
        public void CopyTestResults (string fromProjectDirectory, string toProjectDirectory)
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Copying test results to original directory...");
                Console.WriteLine ("From dir:");
                Console.WriteLine (fromProjectDirectory);
                Console.WriteLine ("To dir:");
                Console.WriteLine (toProjectDirectory);
            }

            var fromTestsDir = fromProjectDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results";

            var destinationTestsDir = toProjectDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results";

            if (Script.IsVerbose) {            
                Console.WriteLine ("Moving test results from:");
                Console.WriteLine ("  " + fromTestsDir);
                Console.WriteLine ("To:");
                Console.WriteLine ("  " + destinationTestsDir);
                Console.WriteLine ("");
            }

            if (Directory.Exists (fromTestsDir)) {

                foreach (var versionDir in Directory.GetDirectories(fromTestsDir)) {
                        
                    if (Script.IsVerbose) {
                        Console.WriteLine ("Version dir:");
                        Console.WriteLine (versionDir);
                        Console.WriteLine ("");
                    }

                    foreach (var formatDir in Directory.GetDirectories(versionDir)) {
                        if (Script.IsVerbose) {
                            Console.WriteLine ("Format dir:");
                            Console.WriteLine (formatDir);
                            Console.WriteLine ("");
                        }

                        var version = Path.GetFileName (versionDir);

                        var format = Path.GetFileName (formatDir);

                        var toDir = destinationTestsDir
                            + Path.DirectorySeparatorChar
                            + version
                            + Path.DirectorySeparatorChar
                            + format;
                        
                        if (Script.IsVerbose) {
                            Console.WriteLine ("");
                            Console.WriteLine ("Dir:");
                            Console.WriteLine ("  " + formatDir);
                            Console.WriteLine ("To dir:");
                            Console.WriteLine ("  " + toDir);
                            Console.WriteLine ("");
                            Console.WriteLine ("Version: " + version);
                            Console.WriteLine ("Format: " + format);
                            Console.WriteLine ("");
                        }

                        Script.CopyDirectory (formatDir, toDir, true);
                    }

                }
            }
            
            if (Script.IsVerbose) {
                Console.WriteLine ("");
            }
        }
        
        public string GetGroupPath ()
        {
            var outputBuilder = new StringBuilder();

            foreach (var s in Script.ScriptStack) {
                outputBuilder.Append(s + "/");
            }

            return outputBuilder.ToString().TrimEnd('/');
        }
    }
}
