using System;
using SoftwareMonkeys.csAnt.Versions;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void InitializeVersionManager(VersionManager versionManager)
        {
            Version = versionManager;
        }
    }
}

