using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt.IO
{
    public class FilesGrabber : IFilesGrabber
    {
        public string OriginalDirectory { get; set; }

        public string CurrentDirectory { get;set; }

        public IFileFinder Finder { get; set; }

        public bool IsVerbose { get;set; }

        public FilesGrabber (
            string originalDirectory,
            string currentDirectory
        )
        {
            OriginalDirectory = originalDirectory;
            CurrentDirectory = currentDirectory;
            Finder = new FileFinder();
        }
        
        public FilesGrabber (
            string originalDirectory,
            string currentDirectory,
            bool isVerbose
        )
        {
            OriginalDirectory = originalDirectory;
            CurrentDirectory = currentDirectory;
            Finder = new FileFinder(isVerbose);
            IsVerbose = isVerbose;
        }
        
        public FilesGrabber (
            string originalDirectory,
            string currentDirectory,
            IFileFinder finder
        )
        {
            OriginalDirectory = originalDirectory;
            CurrentDirectory = currentDirectory;
            Finder = finder;
        }

        public void GrabOriginalScripts (
            params string[] scriptNames
        )
        {
            List<string> list = new List<string> ();

            foreach (var name in scriptNames) {
                list.Add ("/scripts/" + name + ".cs");
                list.Add ("/scripts/" + name + "/**.cs");
            }

            GrabOriginalFiles(
                list.ToArray()
            );
        }

        public void GrabOriginalScriptingFiles ()
        {
            GrabOriginalFiles(
                //"../*.node" // TODO: Check if needed. Not currently supported
                "*.exe",
                "*.node",
                "*.sh",
                "*.bat",
                "*.vbs",
                //"bin/**",
                "lib/csAnt/**",
                //"lib/**",
                //"/src/**.node", // TODO: Check if needed
                //"/src/**.cs",
                //"/src/**.csproj",
                //"/src/**.sln",
                "scripts/**"
            );
        }
        
        public void GrabOriginalFiles ()
        {
            GrabOriginalFiles(
                "*",
                "lib/**",
                "bin/**",
                "src/**.node",
                "src/**.cs",
                "src/**.csproj",
                "src/**.sln",
                "src/**.snk",
                "scripts/**",
                "rls/*.txt",
                "rls/*.zip"//,
                //"../*.node" // TODO: Check if needed. Not currently supported
            );
        }

        public void GrabOriginalFiles (params string[] patterns)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Grabbing original project files...");

            if (IsVerbose) {
                Console.WriteLine ("From:");
                Console.WriteLine ("  " + OriginalDirectory);
                Console.WriteLine ("To:");
                Console.WriteLine ("  " + CurrentDirectory);
                Console.WriteLine ("");

                Console.WriteLine ("Patterns:");
                foreach (var pattern in patterns)
                {
                    Console.WriteLine (pattern);
                }
                
                Console.WriteLine ("");
            }

            int i = 0;

            if (CurrentDirectory != OriginalDirectory) {
                foreach (var file in Finder.FindFiles (OriginalDirectory, patterns)) {
                    if (file.IndexOf(OriginalDirectory) == -1)
                        throw new NotSupportedException("Paths outside the project aren't yet supported.");

                    i++;
                    var toFile = file.Replace (OriginalDirectory, CurrentDirectory);

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
                Console.WriteLine ("OriginalDirectory is the same as CurrentDirectory. No need to grab files.");

            Console.WriteLine("Total files: " + i);

            Console.WriteLine ("");
        }
    }
}

