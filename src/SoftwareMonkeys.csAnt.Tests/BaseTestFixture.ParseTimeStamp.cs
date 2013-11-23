using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public string ExtractTimeStamp()
        {
            return ExtractTimeStamp(WorkingDirectory);
        }

        public string ExtractTimeStamp(string currentDirectory)
        {
            var pos = currentDirectory.IndexOf(".tmp")+5;
            var t = currentDirectory.Substring(pos, currentDirectory.Length-pos);
            pos = t.IndexOf(Path.DirectorySeparatorChar);
            t = t.Substring(0, pos);
            return t;
        }
    }
}

