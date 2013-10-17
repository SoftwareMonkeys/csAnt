using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitSync(string fromDir, string toDir)
		{
			// Git import

			// Blink sync selected files

			// Commit import project

			// Push import project
		}

		public void GitSync(string toDir)
		{
			GitSync(CurrentDirectory, toDir);
		}
	}
}

