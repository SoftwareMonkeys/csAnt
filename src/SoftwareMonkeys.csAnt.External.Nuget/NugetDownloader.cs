using System;
using SoftwareMonkeys.csAnt.IO;
using System.IO;


namespace SoftwareMonkeys.csAnt
{
    public class NugetDownloader
    {
        public FileDownloader Downloader { get;set; }

        public NugetDownloader ()
        {
            Downloader = new FileDownloader();
        }

        public void Download()
        {
            // TODO: Make it possible to keep this path in a config file
            var url = "http://nuget.org/nuget.exe";
            
            // TODO: Make it possible to keep this path in a config file
            var filePath = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                + "nuget.exe";

            Downloader.Download(url, filePath);
        }
    }
}

