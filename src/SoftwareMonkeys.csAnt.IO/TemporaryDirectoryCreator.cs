using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public class TemporaryDirectoryCreator : ITemporaryDirectoryCreator
    {
        private string currentDirectory;
        public string CurrentDirectory {
            get {
                    return currentDirectory;
            }
            set {
                currentDirectory = value;
            }
        }

        public bool IsVerbose { get;set; }

        public string TimeStamp { get;set; }

        public TemporaryDirectoryCreator (string currentDirectory, string timeStamp, bool isVerbose)
        {
            if (String.IsNullOrEmpty(currentDirectory))
                throw new ArgumentException("currentDirectory", "The current directory must be provided.");

            if (String.IsNullOrEmpty(timeStamp))
                throw new ArgumentException("timeStamp", "A time stamp must be provided.");

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
                Console.WriteLine ("Name: " + name);
            }

            path = Path.GetFullPath (
                CurrentDirectory
                + Path.DirectorySeparatorChar
                + ".."
                + Path.DirectorySeparatorChar
                + name
                + ".tmp"
            );

            if (IsVerbose) {
                Console.WriteLine ("Path");
                Console.WriteLine (path);
                Console.WriteLine ("");
            }

            return path;
        }
    }
}

