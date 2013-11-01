using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public bool ImportExists(string importProjectName)
		{
			var path = ImportedDirectory
				+ Path.DirectorySeparatorChar
					+ importProjectName;

			Console.WriteLine ("");
			Console.WriteLine ("Import exists:");
			Console.WriteLine (path);
			Console.WriteLine (Directory.Exists(path));
			Console.WriteLine ("");

			return Directory.Exists(path);
		}
	}
}

