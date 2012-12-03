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
			var cmd = Injection.Retriever.Get<AddLibCommand>(
				new object[]{
					this,
					name,
					zipFileUrl,
					subPath
				}
			);

			cmd.Execute();
		}

	}
}

