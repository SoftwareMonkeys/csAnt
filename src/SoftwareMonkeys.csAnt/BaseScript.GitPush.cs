using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitPush(string remote)
		{
			Git ("push", remote);
		}
		
		public void GitPushFromDirectory(string directory, string remote)
		{
			var originalDirectory = CurrentDirectory;

			CurrentDirectory = directory;

			GitPush(remote);

			CurrentDirectory = originalDirectory;
		}
		
		public void GitPushFromDirectoryToDirectory (string directory, string destination)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Pushing git from:");
			Console.WriteLine (directory);
			Console.WriteLine ("To:");
			Console.WriteLine (destination);
			Console.WriteLine ("");

			GitDirectory (directory, "push", destination);
		}
	}
}

