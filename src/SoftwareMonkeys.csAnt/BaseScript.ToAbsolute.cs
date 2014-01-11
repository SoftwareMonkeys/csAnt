using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string ToAbsolute (string relativePath)
        {
            if (!IsAbsolute (relativePath)) {
                return Path.GetFullPath (relativePath);
            } else {
                var absolutePath = relativePath;
                return absolutePath;
            }
		}
	}
}

