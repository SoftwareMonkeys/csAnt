using System;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string ToRelative(string absolutePath)
		{
            return PathConverter.ToRelative(absolutePath);
		}
	}
}

