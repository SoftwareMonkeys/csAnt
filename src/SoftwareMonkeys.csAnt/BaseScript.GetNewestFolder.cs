using System;
using System.IO;
using System.Linq;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetNewestFolder(string directory)
		{
			string path = directory;

			var directories = new DirectoryInfo(directory).GetDirectories().OrderByDescending(p => p.CreationTime);

			foreach (var d in directories)
			{
				path = d.FullName;
				break;
			}

			return path;
		}
	}
}

