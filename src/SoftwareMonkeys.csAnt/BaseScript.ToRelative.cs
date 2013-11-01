using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string ToRelative(string absolutePath)
		{
			return absolutePath.Replace(CurrentDirectory, "").TrimStart('/').TrimStart('\\');
		}
	}
}

