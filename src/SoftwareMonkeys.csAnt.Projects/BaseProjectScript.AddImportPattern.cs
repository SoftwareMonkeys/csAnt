using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void AddImportPattern(string projectName, string pattern)
		{
			var listPath = ImportStagingDirectory
				+ Path.DirectorySeparatorChar
				+ projectName
				+ Path.DirectorySeparatorChar
				+ "patterns.txt";

			var patterns = new List<string>();

			pattern = pattern.Trim('/').Trim ('\\');
			
			if (File.Exists (listPath))
				patterns.AddRange (File.ReadAllLines(listPath));

			if (!patterns.Contains(pattern))
				patterns.Add (pattern);

			File.WriteAllLines(listPath, patterns.ToArray());
		}
	}
}

