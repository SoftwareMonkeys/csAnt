using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool ImportExists(string importProjectName)
		{
			var path = ImportStagingDirectory
				+ Path.DirectorySeparatorChar
					+ importProjectName;

			Console.WriteLine ("");
			Console.WriteLine ("Import exists:");
			Console.WriteLine (path);
			Console.WriteLine (Directory.Exists(path).ToString());
			Console.WriteLine ("");

			return Directory.Exists(path);
		}
	}
}

