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
			var tmpDir = Path.GetFullPath(
				GetTmpRoot()
				+ Path.DirectorySeparatorChar
				+ Guid.NewGuid().ToString()
			);

			if (!Directory.Exists(tmpDir))
				Directory.CreateDirectory(tmpDir);
				
			return tmpDir;
		}
	}
}

