using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public string AddGitImport (string importProject, string importProjectPath)
		{
			var projectName = ProjectName;

			var importProjectName = importProject;

			var sourceDirectory = Path.GetFullPath (importProjectPath);

			var currentDirectory = CurrentDirectory;

			var parentDirectory = Path.GetDirectoryName (currentDirectory);

			// Create the path to the directory containing the local copy of the import
			var importedDirectory = parentDirectory
				+ Path.DirectorySeparatorChar
				+ projectName
				+ "-Imports"
				+ Path.DirectorySeparatorChar
				+ importProjectName;

			if (IsVerbose) {
				Console.WriteLine ("");
				Console.WriteLine ("Adding Git import...");
				Console.WriteLine ("");

				Console.WriteLine ("Import project: " + importProjectName);

				Console.WriteLine ("Source path: " + sourceDirectory);

				Console.WriteLine ("Current directory: " + currentDirectory);

				Console.WriteLine ("Parent directory: " + parentDirectory);

				Console.WriteLine ("Import directory: " + importedDirectory);

			
				Console.WriteLine ("");
			}
			Directory.CreateDirectory(importedDirectory);

			GitClone(sourceDirectory, importedDirectory);

			return importedDirectory;
		}
	}
}

