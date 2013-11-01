using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public string GetImportedDirectory()
		{
			var parentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(ProjectNode.FilePath));

			var importsDirectory = parentDirectory
				+ Path.DirectorySeparatorChar
				+ Path.GetFileName(CurrentDirectory)
					+ "-Imports";


			return importsDirectory;
		}
	}
}

