using System;
using System.IO;


namespace SoftwareMonkeys.csAnt.IO
{
    public class FileCopier
    {
        public string SourceDirectory { get;set; }
        public string DestinationDirectory { get;set; }

        public IFileFinder Finder { get;set; }

        public bool IsVerbose { get;set; }

        public FileCopier (
            string sourceDirectory,
            string destinationDirectory
        )
        {
            SourceDirectory = sourceDirectory;
            DestinationDirectory = destinationDirectory;
            Finder = new FileFinder();
        }

        public void Copy(params string[] patterns)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Copying files...");

            if (IsVerbose) {
                Console.WriteLine ("From:");
                Console.WriteLine ("  " + SourceDirectory);
                Console.WriteLine ("To:");
                Console.WriteLine ("  " + DestinationDirectory);
                Console.WriteLine ("");

                Console.WriteLine ("Patterns:");
                foreach (var pattern in patterns)
                {
                    Console.WriteLine (pattern);
                }
                
                Console.WriteLine ("");
            }

            int i = 0;

            if (DestinationDirectory != SourceDirectory) {
                foreach (var file in Finder.FindFiles (SourceDirectory, patterns)) {
                    if (file.IndexOf(SourceDirectory) == -1)
                        throw new NotSupportedException("Paths outside the source/destination directory aren't yet supported.");

                    i++;
                    var toFile = file.Replace (SourceDirectory, DestinationDirectory);

                    if (!Directory.Exists (Path.GetDirectoryName (toFile)))
                        Directory.CreateDirectory (Path.GetDirectoryName (toFile));

                    if (IsVerbose)
                    {
                        Console.WriteLine ("From:");
                        Console.WriteLine ("  " + file);
                        Console.WriteLine ("To:");
                        Console.WriteLine ("  " + toFile);
                    }

                    File.Copy (file, toFile, true);
                }
            }
            else
                Console.WriteLine ("SourceDirectory is the same as DestinationDirectory. No need to copy files.");

            Console.WriteLine("Total files: " + i);

            Console.WriteLine ("");
        }
    }
}

