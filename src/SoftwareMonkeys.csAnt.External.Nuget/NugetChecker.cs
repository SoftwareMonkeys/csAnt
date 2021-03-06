using System;
using System.IO;


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
            // TODO: Make it possible to keep this path in a config file
            var filePath = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "nuget.exe";

            if (!File.Exists (filePath))
            {
                Console.WriteLine("nuget.exe file not found at:");
                Console.WriteLine(filePath.Replace(Environment.CurrentDirectory, ""));
                Console.WriteLine("Downloading...");

                Downloader.Download();
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

