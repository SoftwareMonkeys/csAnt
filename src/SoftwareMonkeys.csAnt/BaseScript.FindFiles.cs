using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		/// <summary>
		/// Finds files in the specified directory based on the provided pattern.
		/// If no wildcard is found in the pattern it treats it as a single file name.
		/// If the pattern starts with a slash then the SearchOption.AllDirectories option is NOT used.
		/// If the pattern does NOT start with a slash then the SearchOption.AllDirectories option IS used.
		/// </summary>
		public string[] FindFiles(string baseDirectory, string pattern)
		{
			string[] files = new string[]{};

			// If no wild card was specified then use the pattern as-is, to specify a single file
			if (pattern.IndexOf("*") == -1)
			{
				files = new string[]{
					ProjectDirectory
					+ Path.DirectorySeparatorChar
					+ pattern
				};
			}
			else
			{
				// If it starts with a slash
				if (pattern.StartsWith("/"))
				{
					// Treat it as a full relative path (relative to the project directory)
					// so no need for SearchOption.AllDirectories
					files = Directory.GetFiles (
						ProjectDirectory + "/",
					    	pattern.TrimStart('/') // Trim the slash off the start to make it work
					);
				}
				// Otherwise
				else
				{
					// Treat it only as a partial pattern to match anywhere, so it needs
					// SearchOption.AllDirectories
					files = Directory.GetFiles (
						ProjectDirectory + "/",
					    	pattern,
						SearchOption.AllDirectories
					);
				}
			}

			return files;
		}
	}
}

