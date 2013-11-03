using System;
using System.IO;

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
			Console.WriteLine("");
			Console.WriteLine ("Cloning...");
			Console.WriteLine ("Source: " + sourceDir);
			Console.WriteLine ("Destination: " + destinationDir);

			var tmpDir = GetTmpDir() + Path.DirectorySeparatorChar + "_clone";

			Git (
				"clone",
				sourceDir,
				tmpDir,
				"--verbose"
			);

			MoveDirectory(tmpDir, destinationDir);

			Console.WriteLine("");
			Console.WriteLine("Complete");
			Console.WriteLine("");

		}
	}
}

