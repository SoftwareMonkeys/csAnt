using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void ExportFile (string projectName, string relativePath)
		{
			ExportFile(projectName, relativePath, "/", false);
		}

		public void ExportFile(string projectName, string relativePath, string destination, bool flattenHeirarchy)
		{
			destination = ToAbsolute(destination);

			AddImportPattern(projectName, relativePath);

			Console.WriteLine ("");
			Console.WriteLine ("Exporting files...");
			Console.WriteLine ("Project name:");
			Console.WriteLine (projectName);
			Console.WriteLine ("Path:");
			Console.WriteLine (relativePath);
			Console.WriteLine ("Destination:");
			Console.WriteLine (destination);
			Console.WriteLine ("Flatten path:");
			Console.WriteLine (flattenHeirarchy);
			Console.WriteLine ("");
			Console.WriteLine ("Files:");

			if (!ImportExists (projectName))
				Error ("Import project '" + projectName + "' not found.");
			else {
				ImportRefresh(projectName);

				var importedProjectDirectory = ImportedDirectory
					+ Path.DirectorySeparatorChar
						+ projectName;
				
				Console.WriteLine ("");
				Console.WriteLine ("Relative path:");
				Console.WriteLine (relativePath);
				Console.WriteLine ("");

				foreach (var file in FindFiles(CurrentDirectory, relativePath))
				{
					var fixedPath = ToRelative(relativePath);
					if (flattenHeirarchy)
						fixedPath = Path.GetFileName(relativePath);

					Console.WriteLine ("");
					Console.WriteLine ("Fixed path:");
					Console.WriteLine (fixedPath);
					Console.WriteLine ("");

					var toFile = importedProjectDirectory
						+ Path.DirectorySeparatorChar
						+ fixedPath;
					
					Console.WriteLine ("");
					Console.WriteLine ("Exporting (copying) file:");
					Console.WriteLine (file);
					Console.WriteLine ("To:");
					Console.WriteLine (toFile);
					Console.WriteLine ("");

					EnsureDirectoryExists(Path.GetDirectoryName(toFile));

					File.Copy(file, toFile);

					var sourcePath = File.ReadAllText(importedProjectDirectory + Path.DirectorySeparatorChar + "source.txt");
					
					Console.WriteLine ("Source path:");
					Console.WriteLine (sourcePath);

					GitAddToDirectory(importedProjectDirectory, toFile);

					GitCommitDirectory (importedProjectDirectory, "Exported from '" + ProjectName + "' project.");

					// Get the remote name
					var remoteName = Path.GetFileName(importedProjectDirectory);

					// Add the importable project directory as a remote to the original
					GitAddRemoteToDirectory(sourcePath, remoteName, importedProjectDirectory);

					// Pull changes to back to the original project
					GitPullToDirectory(sourcePath, remoteName);
				}
			}
			
			Console.WriteLine ("");
			Console.WriteLine ("Exporting complete.");
			Console.WriteLine ("");
		}
	}
}

