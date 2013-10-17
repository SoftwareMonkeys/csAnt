using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void Sync(string fromDir, string toDir)
		{
			var exeFile = Path.GetFullPath("lib/blinksync.exe");

			StartProcess(exeFile, fromDir, toDir);
		}
	}
}

