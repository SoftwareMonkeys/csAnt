using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void DownloadAndUnzip(string zipFileUrl, string localDirectory, bool force)
		{
			string tmpFile = GetTmpFile();

			DownloadAndUnzip(zipFileUrl, tmpFile, localDirectory, "/", force);
		}

		public void DownloadAndUnzip (string zipFileUrl, string localDirectory)
		{
			DownloadAndUnzip(zipFileUrl, localDirectory, false);
		}

		public void DownloadAndUnzip(string zipFileUrl, string zipFileLocalPath, string localDirectory, string subPath, bool force)
		{
			var cmd = new DownloadAndUnzipCommand(
				this,
				zipFileUrl,
				zipFileLocalPath,
				localDirectory,
				subPath
			);

			cmd.Force = force;

			ExecuteCommand(cmd);
		}
	}
}

