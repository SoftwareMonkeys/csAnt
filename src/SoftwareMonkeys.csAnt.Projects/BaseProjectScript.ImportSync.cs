using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{

		public void ImportSync(string projectName, string projectPath)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Synchronising files between imported project...");
			Console.WriteLine ("Project:");
			Console.WriteLine (projectName);
			Console.WriteLine ("Path:");
			Console.WriteLine (projectPath);
			Console.WriteLine ("");

			var importedProjectPath = ImportedDirectory
				+ Path.DirectorySeparatorChar
					+ projectName;

			var patternsFile = importedProjectPath
				+ Path.DirectorySeparatorChar
					+ "patterns.txt";

			var patterns = File.ReadAllLines(patternsFile);

			foreach (var pattern in patterns)
			{
				Console.WriteLine ("");
				Console.WriteLine ("Pattern:");
				Console.WriteLine (pattern);
				Console.WriteLine ("");

				Sync (CurrentDirectory, importedProjectPath, pattern);
				GitAddToDirectory(importedProjectPath, pattern);
			}

			/*// Git import
			var importedPath = AddGitImport(projectName, projectPath);

			// Blink sync selected files
			Sync(ProjectDirectory, importedPath);*/
				
			var sourcePath = File.ReadAllText(importedProjectPath + Path.DirectorySeparatorChar + "source.txt");
					

			// Commit import project
			GitCommitDirectory(importedProjectPath, "Sync from '" + ProjectName + "' project.");

			// Get the remote name
			var remoteName = Path.GetFileName(importedProjectPath);

			// Add the importable project directory as a remote to the original
			GitAddRemoteToDirectory(sourcePath, remoteName, importedProjectPath);

			// Pull changes to back to the original project
			GitPullToDirectory(sourcePath, remoteName);
		}

		public void ImportSync(string toDir)
		{
			ImportSync(CurrentDirectory, toDir);
		}
	}
}

