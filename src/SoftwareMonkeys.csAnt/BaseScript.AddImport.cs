using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string AddImport (string importProject, string importProjectPath)
		{
			var folderName = Path.GetFileName(CurrentDirectory);

			var importProjectName = importProject;

			var sourceDirectory = importProjectPath;

			if (!sourceDirectory.Contains("http")
			    && !sourceDirectory.Contains (CurrentDirectory))
				sourceDirectory = Path.GetFullPath (importProjectPath);

			var currentDirectory = CurrentDirectory;

			var parentDirectory = Path.GetDirectoryName (currentDirectory);

			// Create the path to the directory containing the local copy of the import
			var importStagingDirectory = ImportStagingDirectory
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

				Console.WriteLine ("Import directory: " + importStagingDirectory);

				Console.WriteLine ("");
			}

			Directory.CreateDirectory(importStagingDirectory);

			GitClone(sourceDirectory, importStagingDirectory);

			var sourceFile = importStagingDirectory
				+ Path.DirectorySeparatorChar
					+ "source.txt";

			File.WriteAllText(sourceFile, sourceDirectory);

			return importStagingDirectory;
		}
	}
}

