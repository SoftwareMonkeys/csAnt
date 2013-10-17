using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseProjectScript
	{
		public void GitImport()
		{
			var projectName = ProjectName;

			var importProjectName = args[0];

			var sourceDirectory = Path.GetFullPath(args[1]);

			var currentDirectory = CurrentDirectory;

			var parentDirectory = Path.GetDirectoryName(currentDirectory);

			// Create the path to the directory containing the local copy of the import
			var importsDirectory = parentDirectory
				+ Path.DirectorySeparatorChar
				+ projectName
				+ "-Imports"
				+ Path.DirectorySeparatorChar
				+ importProjectName;

			Console.WriteLine ("Import project: " + importProjectName);

			Console.WriteLine ("Source path: " + sourceDirectory);

			Console.WriteLine ("Current directory: " + currentDirectory);

			Console.WriteLine ("Parent directory: " + parentDirectory);

			Console.WriteLine ("Import directory: " + importsDirectory);

			CloneImport(importProjectName, sourceDirectory, importsDirectory);
		}
	}
}

