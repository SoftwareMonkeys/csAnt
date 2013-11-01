using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string ToAbsolute(string relativePath)
		{
			return Path.GetFullPath(relativePath);
		}
	}
}

