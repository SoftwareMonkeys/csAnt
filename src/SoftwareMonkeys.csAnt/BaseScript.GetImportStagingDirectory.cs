using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetImportStagingDirectory()
		{
			var parentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(CurrentNode.FilePath));

			var stagingDirectory = parentDirectory
				+ Path.DirectorySeparatorChar
				+ Path.GetFileName(CurrentDirectory)
					+ "-Imports";


			return stagingDirectory;
		}
	}
}

