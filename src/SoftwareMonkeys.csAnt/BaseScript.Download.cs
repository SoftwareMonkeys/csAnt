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
			var cmd = new DownloadCommand(
				this,
				url,
				localDestination
			);

			ExecuteCommand(cmd);

			return (string)cmd.ReturnValue;
		}
	}
}

