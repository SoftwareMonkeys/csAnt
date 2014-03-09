using System;
using System.Net;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string Download(
			string url,
			string localDestination
		)
		{
            // TODO: Check if this should be injected or an instance kept on a property
            return new FileDownloader().Download(url, localDestination);

			/*var cmd = new DownloadCommand(
				this,
				url,
				localDestination
			);

			ExecuteCommand(cmd);

			return (string)cmd.ReturnValue;*/
		}
	}
}

