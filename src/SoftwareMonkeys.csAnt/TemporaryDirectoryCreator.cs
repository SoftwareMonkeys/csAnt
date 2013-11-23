using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public class TemporaryDirectoryCreator
    {
        public string CurrentDirectory { get;set; }

        public bool IsVerbose { get;set; }

        public string TimeStamp { get;set; }

        public TemporaryDirectoryCreator (string currentDirectory, string timeStamp, bool isVerbose)
        {
            if (String.IsNullOrEmpty(currentDirectory))
                throw new ArgumentException("currentDirectory");

            if (String.IsNullOrEmpty(timeStamp))
                throw new ArgumentException("timeStamp");

            CurrentDirectory = currentDirectory;
            IsVerbose = isVerbose;
            TimeStamp = timeStamp;
        }

        public string GetTmpDir()
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Getting temporary directory...");
            }

            var root = GetTmpRoot ();

            var name = Path.GetFileName (CurrentDirectory);

            var path = String.Empty;

            path = root.TrimEnd(Path.DirectorySeparatorChar)
                + Path.DirectorySeparatorChar
                + TimeStamp
                + Path.DirectorySeparatorChar
                + name;

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Time stamp: " + TimeStamp);
                Console.WriteLine ("");
                Console.WriteLine ("Root:");
                Console.WriteLine (root);
                Console.WriteLine ("Path:");
                Console.WriteLine (path);
                Console.WriteLine ("");
            }

            if (!Directory.Exists (path))
                Directory.CreateDirectory(path);

            return path;
        }

        public string GetTmpRoot()
        {
            var name = Path.GetFileNameWithoutExtension (CurrentDirectory);

            var path = String.Empty;

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Getting temporary root...");
                Console.WriteLine ("Current directory:");
                Console.WriteLine (CurrentDirectory);
                Console.WriteLine ("Name:");
                Console.WriteLine (name);
            }
            // TODO: Clean up
           // var isTmp = CurrentDirectory.Contains(".tmp");

            //if (!isTmp) {
            //    Console.WriteLine ("Not already in tmp dir.");
                path = Path.GetFullPath (
                    CurrentDirectory
                    + Path.DirectorySeparatorChar
                    + ".."
                    + Path.DirectorySeparatorChar
                    + name
                    + ".tmp"
                );
            /*} else {
                Console.WriteLine ("Already in tmp dir.");
                path = Path.GetFullPath (
                    CurrentDirectory
                    + Path.DirectorySeparatorChar
                    + "_tmp"
                );
            }*/

            if (IsVerbose) {
                Console.WriteLine ("Path");
                Console.WriteLine (path);
                Console.WriteLine ("");
            }

            return path;
        }
    }
}

