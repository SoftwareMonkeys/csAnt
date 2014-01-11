using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public bool IsAbsolute (string path)
        {
            if (IsWindows) {
                return path.Contains (":");
            } else {
                // TODO: Check if this is the best way of doing this
                return path.StartsWith (Path.DirectorySeparatorChar.ToString());
            }
        }
    }
}

