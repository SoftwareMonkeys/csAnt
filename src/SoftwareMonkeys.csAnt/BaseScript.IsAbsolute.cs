using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public bool IsAbsolute (string path)
        {
            return PathConverter.IsAbsolute(path);
        }
    }
}

