using System;
using System.Net;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void Download(
			string url,
			string toFile
		)
		{
            // TODO: Check if this should be injected or an instance kept on a property
            new FileDownloader().Download(url, toFile);
		}
	}
}

