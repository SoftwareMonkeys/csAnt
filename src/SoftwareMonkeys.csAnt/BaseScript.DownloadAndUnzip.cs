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
		}
	}
}

