using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void ImportRefresh (string name)
		{
			var dir = ImportStagingDirectory
				+ Path.DirectorySeparatorChar
					+ name;

			GitPullToDirectory(dir, "origin");

		}
	}
}

