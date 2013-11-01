using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetImportedDirectory()
		{
			var parentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(CurrentNode.FilePath));

			var importsDirectory = parentDirectory
				+ Path.DirectorySeparatorChar
				+ Path.GetFileName(CurrentDirectory)
					+ "-Imports";


			return importsDirectory;
		}
	}
}

