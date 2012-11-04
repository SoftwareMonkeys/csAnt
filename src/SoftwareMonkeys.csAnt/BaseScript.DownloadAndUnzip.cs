using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void DownloadAndUnzip(string zipFileUrl, string localDirectory)
		{
			// Create a temporary file name
			var tmpFile = ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "_tmp"
				+ Path.DirectorySeparatorChar
				+ "tmp-" + Guid.NewGuid().ToString() + ".zip";

			// Create the _tmp directory if it doesn't exist
			if (!Directory.Exists(Path.GetDirectoryName(tmpFile)))
				Directory.CreateDirectory(Path.GetDirectoryName(tmpFile));

			// Download the zip file to the temporary location
			Download (zipFileUrl, tmpFile);

			// Unzip the zip file
			Unzip (tmpFile, localDirectory);

			// Delete the temporary file
			File.Delete(tmpFile);
		}
	}
}

