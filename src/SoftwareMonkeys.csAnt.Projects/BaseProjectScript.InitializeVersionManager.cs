using System;
using SoftwareMonkeys.csAnt.Versions;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void InitializeVersionManager (VersionManager versionManager)
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Initializing version manager:");
                Console.WriteLine ("  " + versionManager.GetType().Name);
                Console.WriteLine ("");
            }

            Version = versionManager;
        }
    }
}

