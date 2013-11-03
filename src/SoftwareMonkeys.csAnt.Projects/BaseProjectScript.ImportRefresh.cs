using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void ImportRefresh (string name)
		{
			var dir = ImportedDirectory
				+ Path.DirectorySeparatorChar
					+ name;

			GitPullToDirectory(dir, "origin");

		}
	}
}

