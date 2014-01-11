using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void SetVersion(Version version)
        {
            Version.SetVersion(CurrentNode, version);
        }
    }
}

