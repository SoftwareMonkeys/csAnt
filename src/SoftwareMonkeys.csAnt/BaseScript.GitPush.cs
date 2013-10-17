using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitPush(string remote)
		{
			Git ("push", remote);
		}
		
		public void GitPushDirectory(string directory, string remote)
		{
			var originalDirectory = CurrentDirectory;

			CurrentDirectory = directory;

			GitPush(remote);

			CurrentDirectory = originalDirectory;
		}
	}
}

