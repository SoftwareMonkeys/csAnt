using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void IncrementVersion()
        {
            Version.IncrementVersion(CurrentNode, 4);
        }

        public void IncrementVersion(int position)
        {
            Version.IncrementVersion(CurrentNode, position);
        }
    }
}

