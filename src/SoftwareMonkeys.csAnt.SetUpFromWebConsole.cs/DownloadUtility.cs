using System;
using System.IO;
using System.Net;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole
{
    public class DownloadUtility
    {

        static public void Download (string url, string toFile, bool overwriteFile)
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

                IOUtility.EnsureDirectoryExists (Path.GetDirectoryName (toFile));

                webClient.DownloadFile (
                    url,
                    toFile
                );

                OutputSize (webClient);

                Console.WriteLine ("Download complete.");
                Console.WriteLine ("");
            }
        }

        static public void OutputSize(WebClient webClient)
        {
            var size = Convert.ToInt32 (webClient.ResponseHeaders ["Content-Length"]);

            var sizeString = GetSizeString(size);

            Console.WriteLine ("  Size: " + sizeString);
        }

        static public string GetSizeString(int size)
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

