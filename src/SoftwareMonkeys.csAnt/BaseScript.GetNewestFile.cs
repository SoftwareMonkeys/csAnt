using System;
using System.IO;
using System.Linq;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetNewestFile(string directory)
		{
			string file = String.Empty;

			var files = new DirectoryInfo(directory).GetFiles().OrderByDescending(p => p.CreationTime);

			if (files.Length > 0)
				file = files[0].FullName;

			return file;
		}
	}
}

