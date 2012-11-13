using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void ClearTmp()
		{
			var tmpDir = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "_tmp";

			Directory.Delete(tmpDir, true);
		}
	}
}

