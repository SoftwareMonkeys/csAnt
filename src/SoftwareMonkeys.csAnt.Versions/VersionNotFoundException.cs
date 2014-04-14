using System;

namespace SoftwareMonkeys.csAnt.Versions
{
    public class VersionNotFoundException : Exception
    {
        public VersionNotFoundException (string workingDirectory) : base("The version wasn't found. Ensure a [Project].node file exists in the project root and contains a 'Version' property.")
        {
        }
    }
}

