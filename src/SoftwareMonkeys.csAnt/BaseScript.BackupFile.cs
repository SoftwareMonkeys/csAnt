using System;
using System.IO;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string BackupFile(string relativeFilePath)
		{
			var cmd = Injection.Retriever.Get<BackupFileCommand>(
				new object[]{
					this,
					relativeFilePath
				}
			);

			ExecuteCommand(cmd);

			return (string)cmd.ReturnValue;

		}
	}
}

