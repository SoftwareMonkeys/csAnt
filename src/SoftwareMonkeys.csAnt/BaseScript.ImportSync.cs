using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void ImportSync (string projectName, string projectPath)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Synchronising files between imported project...");
			Console.WriteLine ("Project:");
			Console.WriteLine (projectName);
			Console.WriteLine ("Path:");
			Console.WriteLine (projectPath);
			Console.WriteLine ("");

			var importedProjectPath = ImportStagingDirectory
				+ Path.DirectorySeparatorChar
				+ projectName;

			var patternsFile = importedProjectPath
				+ Path.DirectorySeparatorChar
				+ "patterns.txt";

			if (!File.Exists (patternsFile)) {
				Error ("No import/export patterns have been set for '" + projectName + "' import. Add them by using the ImportFile and ExportFile functions.");
			} else {
				var patterns = File.ReadAllLines (patternsFile);

				foreach (var pattern in patterns) {
					Console.WriteLine ("");
					Console.WriteLine ("Pattern:");
					Console.WriteLine (pattern);
					Console.WriteLine ("");

					Sync (CurrentDirectory, importedProjectPath, pattern);
					GitAddToDirectory (importedProjectPath, pattern);
				}
				
				var sourcePath = File.ReadAllText (importedProjectPath + Path.DirectorySeparatorChar + "source.txt");
					
				// Commit import project
				GitCommitDirectory (importedProjectPath, "Sync from '" + CurrentNode.Name + "'.");

				// Get the remote name
				var remoteName = Path.GetFileName (importedProjectPath);

				// Add the importable project directory as a remote to the original
				GitAddRemoteToDirectory (sourcePath, remoteName, importedProjectPath);

				// Pull changes to back to the original project
				GitPullToDirectory (sourcePath, remoteName);
			}
		}

		public void ImportSync(string toDir)
		{
			ImportSync(CurrentDirectory, toDir);
		}
	}
}

