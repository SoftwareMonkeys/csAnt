using System;
using SoftwareMonkeys.FileNodes;
using System.IO;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void AddLib(string name, string zipFileUrl)
		{
			AddLib(name, zipFileUrl, String.Empty);
		}

		public void AddLib (string name, string zipFileUrl, string subPath)
		{
			Console.WriteLine ("Adding library...");
			Console.WriteLine ("Name: " + name);
			Console.WriteLine ("Url: " + zipFileUrl);
			Console.WriteLine ("Sub path: " + subPath);

			var cmd = new AddLibCommand(
				this,
				name,
				zipFileUrl,
				subPath
			);

			ExecuteCommand(cmd);
		}

	}
}

