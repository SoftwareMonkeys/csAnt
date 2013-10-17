using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public override string GetTmpRoot ()
		{
			return Path.GetFullPath(
				CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ ".."
				+ Path.DirectorySeparatorChar
				+ ProjectName
				+ ".tmp"
			);
		}
	}
}

