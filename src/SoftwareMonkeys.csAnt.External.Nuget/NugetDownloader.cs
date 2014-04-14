using System;
using SoftwareMonkeys.csAnt.IO;
using System.IO;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetDownloader
    {
        public FileDownloader Downloader { get;set; }

        public string NugetPath = "http://nuget.org/nuget.exe";

        public NugetDownloader ()
        {
            Downloader = new FileDownloader();
        }

        public void Download()
        {
            // TODO: Make it possible to keep this path in a config file or at least on a property
            var filePath = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                + "nuget.exe";

            if (IsOnline(NugetPath))
            {
                Console.WriteLine("Downloading nuget from:");
                Console.WriteLine(NugetPath);
                Downloader.Download(NugetPath, filePath);
            }
            else
            {
                Console.WriteLine("Copying nuget file from:");
                Console.WriteLine(NugetPath);
                DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(filePath));
                File.Copy(NugetPath, filePath);
            }
        }

        public bool IsOnline(string nugetPath)
        {
            return nugetPath.ToLower().StartsWith("http");
        }
    }
}

