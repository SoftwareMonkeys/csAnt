using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void ImportFile (string importProjectName, string relativePath, string destination, bool flattenPath)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Importing files...");
			Console.WriteLine ("Project name:");
			Console.WriteLine (importProjectName);
			Console.WriteLine ("Path:");
			Console.WriteLine (relativePath);
			Console.WriteLine ("Destination:");
			Console.WriteLine (destination);
			Console.WriteLine ("Flatten path:");
			Console.WriteLine (flattenPath);
			Console.WriteLine ("");
			Console.WriteLine ("Files:");

			if (!ImportExists (importProjectName))
				Error ("Import project '" + importProjectName + "' not found.");
			else {
				var importDir = ImportsDirectory
					+ Path.DirectorySeparatorChar
						+ importProjectName;

				foreach (var file in FindFiles(importDir, relativePath))
				{
					var fixedPath = relativePath;
					if (flattenPath)
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
		}
	}
}

