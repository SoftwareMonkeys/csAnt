using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
        public string[] FindFiles (params string[] patterns)
		{
			return FileFinder.FindFiles(CurrentDirectory, patterns);
		}

		public string[] FindFiles (string baseDirectory, params string[] patterns)
        {
            return FileFinder.FindFiles(baseDirectory, patterns);
		}

		public string[] FindFiles (string pattern)
		{           
            return FileFinder.FindFiles(CurrentDirectory, pattern);
		}

		/// <summary>
		/// Finds files in the specified directory based on the provided pattern.
		/// If no wildcard is found in the pattern it treats it as a single file name.
		/// If the pattern starts with a slash then the SearchOption.AllDirectories option is NOT used.
		/// If the pattern does NOT start with a slash then the SearchOption.AllDirectories option IS used.
		/// </summary>
		public string[] FindFiles (string baseDirectory, string pattern)
        {
            return FileFinder.FindFiles(baseDirectory, pattern);

		}
	}
}

