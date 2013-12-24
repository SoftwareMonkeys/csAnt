using System;

namespace SoftwareMonkeys.csAnt.Packages
{
    static public class HttpUtility
    {
        static public bool IsHttpPath(string path)
        {
            return path.StartsWith("http")
                || path.StartsWith("https");
        }
    }
}

