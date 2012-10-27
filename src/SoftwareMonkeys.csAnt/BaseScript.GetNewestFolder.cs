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

			foreach (var d in directories)
			{
				path = d.FullName;
				break;
			}

			return path;
		}
	}
}

