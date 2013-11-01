using System;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void GitSync (string projectName, string projectPath)
		{
			GitSync(projectName, projectPath, "**");
		}

		public void GitSync(string projectName, string projectPath, string filePattern)
		{
			// Git import
			var importedPath = AddGitImport(projectName, projectPath);

			// Blink sync selected files
			Sync(ProjectDirectory, importedPath);

			// Commit import project
			GitCommitDirectory(importedPath, "Committing changes after sync with '" + ProjectName + "' project.");

			// Push import project
			GitPushFromDirectory(importedPath, "origin");
		}

		public void GitSync(string toDir)
		{
			GitSync(CurrentDirectory, toDir);
		}
	}
}

