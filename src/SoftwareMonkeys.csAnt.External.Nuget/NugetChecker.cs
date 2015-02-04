using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetChecker
    {
        public NugetDownloader Downloader { get;set; }
        public string NugetPath
        {
            get { return Downloader.NugetPath; }
            set { Downloader.NugetPath = value; }
        }

        public NugetChecker ()
        {
            Downloader = new NugetDownloader();
        }
        
        public virtual void CheckNuget()
        {
            CheckNuget (String.Empty);
        }

        public virtual void CheckNuget(string nugetSourcePath)
        {
            var libDirectory = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "lib";

            // TODO: Make it possible to keep this path in a config file
            var filePath = libDirectory
                    + Path.DirectorySeparatorChar
                    + "nuget.exe";

            if (!File.Exists (filePath))
            {
                Console.WriteLine("nuget.exe file not found at:");
                Console.WriteLine(filePath.Replace(Environment.CurrentDirectory, ""));

                var found = false;

                // Try finding the nuget.exe file locally if nugetSourcePath is provided
                if (!String.IsNullOrEmpty (nugetSourcePath)) {
                    Console.WriteLine ("Grabbing nuget from: " + nugetSourcePath);
                    var nugetFilePath = Path.Combine (nugetSourcePath, "nuget.exe");
                    if (File.Exists (nugetFilePath)) {
                        var nugetToFilePath = nugetFilePath.Replace (nugetSourcePath, libDirectory);
                        DirectoryChecker.EnsureDirectoryExists (libDirectory);
                        File.Copy (nugetFilePath, nugetToFilePath);
                        found = true;
                    }
                }

                // If not found locally then download the file
                if (!found)
                {
                    Console.WriteLine ("Downloading...");

                    Downloader.Download ();
                }
            }
            else
            {
                Console.WriteLine("nuget.exe file found at:");
                Console.WriteLine(filePath.Replace(Environment.CurrentDirectory, ""));
                Console.WriteLine("Skipping download.");
            }
        }
    }
}

