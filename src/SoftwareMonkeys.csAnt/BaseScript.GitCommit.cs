using System;
namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitCommit ()
		{
			GitCommit ("Committing added/changed files");
		}
		
		public void GitCommit (string message)
		{
			Console.WriteLine ("Committing added/changed files...");

			Git ("commit", "-a", "-m:'" + message + "'");
		}
		
		public void GitCommitDirectory (string directory)
		{
			GitCommitDirectory ("");
		}
		
		public void GitCommitDirectory (string directory, string message)
		{
			Console.WriteLine ("Committing added/changed files...");

			GitDirectory (directory, "commit", "-a", "-m:'" + message + "'");
		}
	}
}

