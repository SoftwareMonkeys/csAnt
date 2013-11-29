using System;
using System.IO;
using System.Net;

namespace SoftwareMonkeys.csAnt.InstallConsole
{
	public static class Utilities
	{
		public static void Download(string url, string toFile)
		{
            if (Path.GetFullPath(toFile) != toFile)
                toFile = Path.GetFullPath(toFile);

			Console.WriteLine ("Downloading...");
			Console.WriteLine ("  From URL: " + url);
			
			Console.WriteLine ("  To file: " + toFile);

			WebClient webClient = new WebClient();

			webClient.Headers.Add("USER-AGENT", "csAnt");

			webClient.Credentials = CredentialCache.DefaultCredentials;

			Console.WriteLine ("  Please wait...(this may take some time)...");

			EnsureDirectoryExists(Path.GetDirectoryName(toFile));

			webClient.DownloadFile(
				url,
				toFile
			);

			var size = Convert.ToInt32(webClient.ResponseHeaders["Content-Length"]);

			var sizeString = size + "b";

			if (size > 1000*1000)
				sizeString = size / 1000 / 1000 + "mb";
			else if (size > 1000)
				sizeString = size / 1000 + "kb";

			Console.WriteLine ("  Size: " + sizeString);

			Console.WriteLine ("Download complete.");
		}

		public static void EnsureDirectoryExists(
			string directoryPath
		)
		{
			// Check parent directories, up to the last one
			if (directoryPath.IndexOf(Path.DirectorySeparatorChar) != directoryPath.LastIndexOf(Path.DirectorySeparatorChar))
				EnsureDirectoryExists(Path.GetDirectoryName(directoryPath));

			if (!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);
		}
	}
}

