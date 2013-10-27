using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual string GetTmpRoot()
		{
			var name = Path.GetFileName(CurrentDirectory);

			return Path.GetFullPath(
				CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ ".."
				+ Path.DirectorySeparatorChar
				+ name
				+ ".tmp"
			);
		}
	}
}

