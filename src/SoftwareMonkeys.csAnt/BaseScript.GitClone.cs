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

			//var originalDirectory = CurrentDirectory;
			
			Console.WriteLine("");
			Console.WriteLine ("Cloning...");
			Console.WriteLine ("Source: " + sourceDir);
			Console.WriteLine ("Destination: " + destinationDir);

			//CurrentDirectory = destinationDir;

			Git (
				"clone",
				sourceDir,
				destinationDir,
				"--verbose"
			);
			
			Console.WriteLine("");
			Console.WriteLine("Complete");
			Console.WriteLine("");

			//CurrentDirectory = originalDirectory;
		}
	}
}

