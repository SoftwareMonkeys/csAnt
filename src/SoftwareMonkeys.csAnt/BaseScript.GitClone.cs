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
			/*Repository.Clone(
				sourceDir,
				destinationDir
			);*/

			Console.WriteLine ("Cloning...");
			Console.WriteLine ("Source: " + sourceDir);
			Console.WriteLine ("Destination: " + destinationDir);

			CurrentDirectory = destinationDir;

			Git (
				"clone",
				sourceDir,
				destinationDir,
				"--verbose"
			);

			Console.WriteLine("Complete");
		}
	}
}

