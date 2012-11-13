using System;
using System.IO;


namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetTmpFile()
		{
			return GetTmpDir()
				+ Path.DirectorySeparatorChar
				+ Guid.NewGuid().ToString();
		}
	}
}

