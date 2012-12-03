using System;
using System.Net;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string Download(
			string url,
			string localDestination
		)
		{
			var cmd = Injection.Retriever.Get<DownloadCommand>(
				new object[]{
					this,
					url,
					localDestination
				}
			);

			ExecuteCommand(cmd);

			return (string)cmd.ReturnValue;

			/*Console.WriteLine ("Downloading...");
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

			return toFile;*/
		}
	}
}

