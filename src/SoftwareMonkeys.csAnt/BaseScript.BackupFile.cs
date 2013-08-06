using System;
using System.IO;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string BackupFile(string relativeFilePath)
		{
			var cmd = new BackupFileCommand(
				this,
				relativeFilePath
			);

			ExecuteCommand(cmd);

			return (string)cmd.ReturnValue;

		}
	}
}

