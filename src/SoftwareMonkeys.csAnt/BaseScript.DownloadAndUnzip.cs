using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void DownloadAndUnzip(string zipFileUrl, string localDirectory)
		{
			string tmpFile = GetTmpFile();

			DownloadAndUnzip(zipFileUrl, tmpFile, localDirectory, "/", false);
		}

		public void DownloadAndUnzip(string zipFileUrl, string zipFileLocalPath, string localDirectory, string subPath, bool force)
		{
			var cmd = Injection.Retriever.Get<DownloadAndUnzipCommand>(
				new object[]
				{
					this,
					zipFileUrl,
					zipFileLocalPath,
					localDirectory,
					subPath
				}
			);

			cmd.Force = force;

			ExecuteCommand(cmd);

			/*// Create the _tmp directory if it doesn't exist
			if (!Directory.Exists(Path.GetDirectoryName(zipFileLocalPath)))
				Directory.CreateDirectory(Path.GetDirectoryName(zipFileLocalPath));

			Console.WriteLine ("Zip file path: " + zipFileLocalPath);

			// If the zip file doesn't already exist
			if (!File.Exists(zipFileLocalPath)
			    // Or the force flag is true
				|| force)
			{
				// Download the zip file to the temporary location
				Download (zipFileUrl, zipFileLocalPath);
			}
			else
			{				
				Console.WriteLine("Zip file already found locally. Skipping download.");
			}

			// Create a temporary folder name
			var tmpFolder = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "_tmp"
				+ Path.DirectorySeparatorChar
				+ "tmp-" + Guid.NewGuid().ToString();

			// Unzip the zip file
			Unzip (zipFileLocalPath, tmpFolder);

			var fullSubPath = Path.GetFullPath(
				tmpFolder
				+ Path.DirectorySeparatorChar
				+ subPath
			);
			
			Console.WriteLine ("");
			Console.WriteLine ("Moving files to: ");
			Console.WriteLine ("  " + localDirectory);
			Console.WriteLine ();

			MoveDirectory(fullSubPath, localDirectory);
			
			// Delete the temporary folder
			if (Directory.Exists(tmpFolder))
				Directory.Delete(tmpFolder);*/
		}
	}
}

