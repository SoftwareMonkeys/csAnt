using System;

namespace SoftwareMonkeys.csAnt
{
    public class ScriptsDirectoryNotFoundException : Exception
    {
        public ScriptsDirectoryNotFoundException (string scriptsDir) : base("The scripts directory was not found: " + scriptsDir)
        {
        }
    }
}

