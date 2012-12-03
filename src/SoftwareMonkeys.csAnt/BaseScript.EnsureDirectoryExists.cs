using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void EnsureDirectoryExists(
			string directoryPath
		)
		{
			if (!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);
		}
	}
}

