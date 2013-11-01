using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void ImportFile (string projectName, string relativePath)
		{
			ImportFile(projectName, relativePath, "/", false);
		}

		public void ImportFile (string projectName, string relativePath, string destination, bool flattenHeirarchy)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Importing files...");
			Console.WriteLine ("Project name:");
			Console.WriteLine (projectName);
			Console.WriteLine ("Path:");
			Console.WriteLine (relativePath);
			Console.WriteLine ("Destination:");
			Console.WriteLine (destination);
			Console.WriteLine ("Flatten path:");
			Console.WriteLine (flattenHeirarchy);
			Console.WriteLine ("");
			Console.WriteLine ("Files:");
			
			AddImportPattern(projectName, relativePath);

			if (!ImportExists (projectName))
				Error ("Import project '" + projectName + "' not found.");
			else {
				var importDir = ImportedDirectory
					+ Path.DirectorySeparatorChar
						+ projectName;

				if (Directory.Exists(importDir))
				{
					foreach (var file in FindFiles(importDir, relativePath))
					{
						var fixedPath = relativePath;
						if (flattenHeirarchy)
							fixedPath = Path.GetFileName(relativePath);

						var toFile = CurrentDirectory
							+ Path.DirectorySeparatorChar
							+ fixedPath;
						
						Console.WriteLine ("");
						Console.WriteLine ("Copying file:");
						Console.WriteLine (file);
						Console.WriteLine ("To:");
						Console.WriteLine (toFile);
						Console.WriteLine ("");

						EnsureDirectoryExists(Path.GetDirectoryName(toFile));

						File.Copy(file, toFile);
					}
				}
				else
					Console.WriteLine ("Import directory not found: " + importDir);
			}
		}
	}
}

