using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual string GetTmpRoot()
		{
			var name = Path.GetFileName(CurrentDirectory);

            var path = String.Empty;

            if (!CurrentDirectory.Contains(".tmp"))
            {
                path = Path.GetFullPath(
                    CurrentDirectory
                    + Path.DirectorySeparatorChar
                    + ".."
                    + Path.DirectorySeparatorChar
                    + name
                    + ".tmp"
                );
            }
            else
            {
                path = Path.GetFullPath(
                    CurrentDirectory
                    + Path.DirectorySeparatorChar
                    + ".."
                    + Path.DirectorySeparatorChar
                    + ".."
                );
            }

            return path;
		}
	}
}

