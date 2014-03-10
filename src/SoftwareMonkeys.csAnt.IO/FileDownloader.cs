using System;
using System.Net;
using System.IO;


namespace SoftwareMonkeys.csAnt.IO
{
    public class FileDownloader
    {
        public FileDownloader ()
        {
        }
        
        public void Download (string url, string toFile)
        {
            Download (url, toFile, false);
        }

        public void Download (string url, string toFile, bool overwriteFile)
        {
        
            if (
                !File.Exists (toFile)
                || overwriteFile
            ) {

                if (Path.GetFullPath (toFile) != toFile)
                    toFile = Path.GetFullPath (toFile);

                Console.WriteLine ("Downloading:");
                Console.WriteLine ("  " + url);
                Console.WriteLine ("To:");
                Console.WriteLine ("  " + toFile);

                WebClient webClient = new WebClient ();

                webClient.Headers.Add ("USER-AGENT", "csAnt");

                webClient.Credentials = CredentialCache.DefaultCredentials;

                Console.WriteLine ("  Please wait...(this may take some time)...");

                DirectoryChecker.EnsureDirectoryExists (Path.GetDirectoryName (toFile));

                webClient.DownloadFile (
                    url,
                    toFile
                );

                OutputSize (webClient);

                Console.WriteLine ("Download complete.");
                Console.WriteLine ("");
            }
        }
        
        public void OutputSize(WebClient webClient)
        {
            var size = Convert.ToInt32 (webClient.ResponseHeaders ["Content-Length"]);

            var sizeString = GetSizeString(size);

            Console.WriteLine ("  Size: " + sizeString);
        }

        public string GetSizeString(int size)
        {
            var sizeString = size + "b";

            if (size > 1000 * 1000)
                sizeString = size / 1000 / 1000 + "mb";
            else if (size > 1000)
                sizeString = size / 1000 + "kb";

            return sizeString;
        }

    }
}

