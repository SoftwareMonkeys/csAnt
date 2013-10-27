using System;
using SoftwareMonkeys.csAnt.Commands;
using System.IO;
using System.Net;

namespace SoftwareMonkeys.csAnt
{
	public class DownloadCommand : BaseScriptCommand
	{
		public string DownloadUrl { get;set; }

		public string Destination { get;set; }

		public DownloadCommand (
			IScript script,
			string downloadUrl,
			string destination
		)
			: base(
				script
			)
		{
			DownloadUrl = downloadUrl;

			Destination = destination;
		}

		public override void Execute ()
		{
			Download (
				DownloadUrl,
				Destination
			);
		}

		public string Download(
			string url,
			string localDestination
		)
		{
			Console.WriteLine ("Downloading...");
			Console.WriteLine ("  From URL: " + url);

			var fileName = Path.GetFileName(url);

			var toFile = localDestination;

			if (localDestination.IndexOf("/") == localDestination.Length-1)
				toFile = localDestination + fileName;
			
			Console.WriteLine ("  To file: " + toFile);

			WebClient webClient = new WebClient();

			webClient.Headers.Add("USER-AGENT", "csAnt");

			webClient.Credentials = CredentialCache.DefaultCredentials;

			Console.WriteLine ("  Please wait...(this may take some time)...");

			Script.EnsureDirectoryExists(Path.GetDirectoryName(localDestination));

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

			return toFile;
		}
	}
}

