using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetTemporaryDirectory()
		{
			return GetTmpDir();
		}

		public string GetTmpDir()
		{
			return Path.GetFullPath(
				CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ ".."
				+ Path.DirectorySeparatorChar
				+ "_tmp"
				+ Path.DirectorySeparatorChar
				+ Guid.NewGuid().ToString()
				);
				
		}
	}
}

