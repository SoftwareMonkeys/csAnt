using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void ImportFile (string projectName, string relativePath)
		{
			ImportFile(projectName, relativePath, "/", false);
		}

		public void ImportFile (string projectName, string relativePath, string destination, bool flattenHeirarchy)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Importing files...");
			Console.WriteLine ("Project name: " + projectName);
			Console.WriteLine ("Path:");
			Console.WriteLine (relativePath);
			if (IsVerbose) {
				Console.WriteLine ("Destination:");
				Console.WriteLine (destination);
				Console.WriteLine ("Flatten path:");
				Console.WriteLine (flattenHeirarchy.ToString());
				Console.WriteLine ("");
			}
			
			AddImportPattern(projectName, relativePath);

			if (!ImportExists (projectName))
				Error ("Import project '" + projectName + "' not found.");
			else {
				ImportRefresh(projectName);

				var importDir = ImportStagingDirectory
					+ Path.DirectorySeparatorChar
						+ projectName;

				if (Directory.Exists(importDir))
				{
					foreach (var file in FindFiles(importDir, relativePath))
					{
						var toFile = file.Replace (importDir, CurrentDirectory);

                        // TODO: Implement flattenHeirarchy
                        //if (flattenHeirarchy)
                            //toFile = toFile.Replace (
						
						Console.WriteLine ("");
						Console.WriteLine ("Copying file:");
						Console.WriteLine (file);
						if (IsVerbose) {
							Console.WriteLine ("To:");
							Console.WriteLine (toFile);
						}
						Console.WriteLine ("");

						EnsureDirectoryExists(Path.GetDirectoryName(toFile));

						if (File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile))
						{
							File.Copy(file, toFile, true);
							if (IsVerbose)
								Console.WriteLine ("File is newer. Using.");
						}
						else if (File.GetLastWriteTime(file) == File.GetLastWriteTime(toFile))
						{
							if (IsVerbose)
								Console.WriteLine ("File is same age. Skipping.");
						}
						else
						{
							if (IsVerbose)
								Console.WriteLine ("File is older. Skipping.");
						}
					}
				}
				else
					Console.WriteLine ("Import directory not found: " + importDir);
			}
		}
	}
}

