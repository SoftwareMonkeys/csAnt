using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitPull(string remote)
		{
			Git ("pull", remote);
		}

		public void GitPull()
		{
			Git ("pull", "-all");
		}
		

		public void GitPullToDirectory(string directory, string remote)
		{
			GitDirectory (directory, "pull", remote, "master");
		}
	}
}

