using System;
namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitCommit ()
		{
			GitCommit ("Committing changed files");
		}
		
		public void GitCommit (string message)
		{
			Console.WriteLine ("Committing changed files...");

			Git ("commit", "-a", "-m:'" + message + "'");
		}
	}
}

