using System;
using System.IO;


namespace SoftwareMonkeys.csAnt
{
    public class NugetChecker
    {
        public NugetDownloader Downloader { get;set; }

        public NugetChecker ()
        {
            Downloader = new NugetDownloader();
        }

        public void CheckNuget()
        {
            // TODO: Make it possible to keep this path in a config file
            var filePath = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "nuget.exe";

            if (!File.Exists (filePath))
                Downloader.Download();
        }
    }
}

