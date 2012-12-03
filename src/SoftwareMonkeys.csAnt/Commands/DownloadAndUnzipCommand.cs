using System;
using SoftwareMonkeys.csAnt.Commands;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	[ScriptCommand]
	public class DownloadAndUnzipCommand : BaseScriptCommand
	{
		public string DownloadUrl { get;set; }

		public string UnzipDestination { get;set; }

		public string ZipFileLocalPath { get;set; }

		public string SubPath { get;set; }

		public bool Force { get;set; }

		public DownloadAndUnzipCommand(
			IScript script,
			string downloadUrl,
			string unzipDestination
		)
			: base(
				script
			)
		{
			DownloadUrl = downloadUrl;

			UnzipDestination = unzipDestination;
		}
		
		public DownloadAndUnzipCommand(
			IScript script,
			string downloadUrl,
			string zipFileLocalPath,
			string unzipDestination,
			string subPath
		)
			: base(
				script
			)
		{
			DownloadUrl = downloadUrl;

			ZipFileLocalPath = zipFileLocalPath;

			UnzipDestination = unzipDestination;
			
			SubPath = subPath;
		}

		public override void Execute ()
		{
			// TODO: Add the ability to use additional parameters
			DownloadAndUnzip(
				DownloadUrl,
				UnzipDestination
			);
		}

		public void DownloadAndUnzip(string zipFileUrl, string localDirectory)
		{
			string tmpFile = Script.GetTmpFile();

			DownloadAndUnzip(zipFileUrl, tmpFile, localDirectory, "/", false);
		}

		public void DownloadAndUnzip(string zipFileUrl, string zipFileLocalPath, string localDirectory, string subPath, bool force)
		{
			// Create the _tmp directory if it doesn't exist
			if (!Directory.Exists(Path.GetDirectoryName(zipFileLocalPath)))
				Directory.CreateDirectory(Path.GetDirectoryName(zipFileLocalPath));

			Console.WriteLine ("Zip file path: " + zipFileLocalPath);

			if (
				// If the zip file doesn't already exist
				!File.Exists(zipFileLocalPath)
			    // Or the force flag is true
				|| force
			)
			{
				// Download the zip file to the temporary location
				Script.Download (zipFileUrl, zipFileLocalPath);
			}
			else
			{				
				Console.WriteLine("Zip file already found locally. Skipping download.");
			}

			// Unzip the zip file
			Script.Unzip (zipFileLocalPath, localDirectory, subPath);

			// TODO: Remove. Should be obsolete
			/*// Create a temporary folder name
			var tmpFolder = Script.CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "_tmp"
				+ Path.DirectorySeparatorChar
				+ "tmp-" + Guid.NewGuid().ToString();

			// Unzip the zip file
			Script.Unzip (zipFileLocalPath, tmpFolder);

			var fullSubPath = Path.GetFullPath(
				tmpFolder
				+ Path.DirectorySeparatorChar
				+ subPath
			);
			
			Console.WriteLine ("");
			Console.WriteLine ("Moving files to: ");
			Console.WriteLine ("  " + localDirectory);
			Console.WriteLine ();

			Script.MoveDirectory(fullSubPath, localDirectory);
			
			// Delete the temporary folder
			if (Directory.Exists(tmpFolder))
				Directory.Delete(tmpFolder);*/
		}
	}
}

