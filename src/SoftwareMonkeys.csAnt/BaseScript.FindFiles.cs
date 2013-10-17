using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string[] FindFiles (string[] patterns)
		{
			return FindFiles(CurrentDirectory, patterns);
		}

		public string[] FindFiles (string baseDirectory, string[] patterns)
		{
			List<string> files = new List<string>();

			foreach (string pattern in patterns)
				foreach (string file in FindFiles(baseDirectory, pattern))
					if (!files.Contains(file))
						files.Add (file);

			return files.ToArray();
		}

		public string[] FindFiles (string pattern)
		{
			return FindFiles(CurrentDirectory, pattern);
		}

		/// <summary>
		/// Finds files in the specified directory based on the provided pattern.
		/// If no wildcard is found in the pattern it treats it as a single file name.
		/// If the pattern starts with a slash then the SearchOption.AllDirectories option is NOT used.
		/// If the pattern does NOT start with a slash then the SearchOption.AllDirectories option IS used.
		/// </summary>
		public string[] FindFiles (string baseDirectory, string pattern)
		{
			List<string> foundFiles = new List<string> ();
			
			if (pattern.IndexOf (baseDirectory) == -1) {
				pattern = baseDirectory.TrimEnd (Path.DirectorySeparatorChar)
					+ Path.DirectorySeparatorChar
					+ pattern.Trim (Path.DirectorySeparatorChar);
			}

			if (IsVerbose) 
				Console.WriteLine ("Pattern: " + pattern);

			var subPath = pattern.Substring (
				0,
				pattern.LastIndexOf (Path.DirectorySeparatorChar)
			);
			
			if (IsVerbose)
				Console.WriteLine ("Sub path: " + subPath);

			var subPattern = pattern;

			if (subPattern.IndexOf (Path.DirectorySeparatorChar) > -1) {
				subPattern = pattern.Substring (
				pattern.LastIndexOf (Path.DirectorySeparatorChar),
				pattern.Length - pattern.LastIndexOf (Path.DirectorySeparatorChar)
				).Trim (Path.DirectorySeparatorChar);
			}
				
			if (IsVerbose)
				Console.WriteLine ("Sub pattern: " + subPattern);

			var searchOption = SearchOption.TopDirectoryOnly;

			if (subPattern == "**") {
				subPattern = "*";
				searchOption = SearchOption.AllDirectories;
			}

			foreach (var file in Directory.GetFiles (subPath, subPattern, searchOption))
			{
				if (!foundFiles.Contains(file))
					foundFiles.Add(file);
			}

			return foundFiles.ToArray();


	        // This method assumes that the application has discovery permissions 
	        // for all folders under the specified path.
	     //   IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles(subPattern, System.IO.SearchOption.AllDirectories);
			// TODO: Tidy up this function

			/*if (IsVerbose) {
				Console.WriteLine ("Finding files...");
				Console.WriteLine ("Pattern: " + pattern);
				Console.WriteLine ("Base directory: " + baseDirectory);
			}

			if (pattern.IndexOf (CurrentDirectory) == -1) {
				pattern = baseDirectory.TrimEnd (Path.DirectorySeparatorChar)
					+ Path.DirectorySeparatorChar
					+ pattern.Trim (Path.DirectorySeparatorChar);
			}

			if (IsVerbose) 
				Console.WriteLine ("Pattern: " + pattern);

			var subPath = pattern.Substring(
				0,
				pattern.LastIndexOf (Path.DirectorySeparatorChar)
			);
			
			if (IsVerbose)
				Console.WriteLine ("Sub path: " + subPath);

			var subPattern = pattern.Substring(
				pattern.LastIndexOf (Path.DirectorySeparatorChar),
				pattern.Length-pattern.LastIndexOf (Path.DirectorySeparatorChar)
			).Trim (Path.DirectorySeparatorChar);
				
			if (IsVerbose)
				Console.WriteLine ("Sub pattern: " + subPattern);

			string[] files = new string[]{};

			if (pattern.EndsWith("**"))
			{
				files = Directory.GetFiles (
					subPath,
				    subPattern.Replace ("**", "*"),
					SearchOption.AllDirectories
				);
			}
			// If no wild card was specified then use the pattern as-is, to specify a single file
			else if (!pattern.EndsWith ("*")) {

				files = new string[]{
					CurrentDirectory
					+ Path.DirectorySeparatorChar
					+ pattern
				};
				
				if (IsVerbose) {
					Console.WriteLine ("Single file:");
					Console.WriteLine (files [0]);
				}
			}
			else
			{
				// If it starts with a slash
				if (pattern.StartsWith("/"))
				{
					// Treat it as a full relative path (relative to the project directory)
					// so no need for SearchOption.AllDirectories
					files = Directory.GetFiles (
						subPath,
					    subPattern.TrimStart('/') // Trim the slash off the start to make it work
					);
					
					if (IsVerbose) {
						Console.WriteLine ("Begins with slash, therefore is relative path.");
					}
				}
				// Otherwise
				else
				{
					// Treat it only as a partial pattern to match anywhere, so it needs
					// SearchOption.AllDirectories
					files = Directory.GetFiles (
						subPath,
					   	subPattern,
						SearchOption.AllDirectories
					);
					
					if (IsVerbose) {
						Console.WriteLine ("Partial path.");
					}
				}
			}

			if (IsVerbose)
				foreach (string file in files)
					Console.WriteLine (file);

			return files;*/
		}
	}
}

