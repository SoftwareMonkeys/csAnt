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
		}
	}
}

