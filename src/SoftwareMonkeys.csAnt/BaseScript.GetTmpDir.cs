using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual string GetTemporaryDirectory()
		{
			return GetTmpDir();
		}

		public virtual string GetTmpDir()
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

