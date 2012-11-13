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
			return CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "_tmp"
				+ Path.DirectorySeparatorChar
				+ Guid.NewGuid().ToString();
				
		}
	}
}

