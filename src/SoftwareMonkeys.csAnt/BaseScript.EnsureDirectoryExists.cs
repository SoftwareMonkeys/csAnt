using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void EnsureDirectoryExists(
			string directoryPath
		)
		{
            DirectoryChecker.EnsureDirectoryExists(directoryPath);
		}
	}
}

