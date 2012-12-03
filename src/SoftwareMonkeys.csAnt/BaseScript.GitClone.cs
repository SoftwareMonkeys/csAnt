using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitClone(
			string sourceDir
		)
		{
			GitClone(
				sourceDir,
				CurrentDirectory
			);
		}

		public void GitClone(
			string sourceDir,
			string destinationDir
		)
		{
			Git (
				"clone",
				sourceDir
			);
		}
	}
}

