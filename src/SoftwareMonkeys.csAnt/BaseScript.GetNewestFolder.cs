using System;
using System.IO;
using System.Linq;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetNewestFolder(string directory)
		{
			string path = String.Empty;

			var directories = new DirectoryInfo(directory).GetDirectories().OrderByDescending(p => p.CreationTime);

			if (directories.Length > 0)
				path = directories[0].FullName;

			return path;
		}
	}
}

