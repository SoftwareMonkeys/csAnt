using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public override string GetTmpDir()
		{
			var path = GetTmpRoot ()
				+ Path.DirectorySeparatorChar
				+ GetTimeStamp()
				+ Path.DirectorySeparatorChar
				+ ProjectName;

			EnsureDirectoryExists(path);

			return path;
		}
	}
}

