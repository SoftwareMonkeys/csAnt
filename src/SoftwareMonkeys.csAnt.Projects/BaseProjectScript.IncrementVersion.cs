using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void IncrementVersion()
        {
            Nodes.EnsureNodes();

            Version.IncrementVersion(CurrentNode, 4);
        }

        public void IncrementVersion(int position)
        {
            Nodes.EnsureNodes();

            Version.IncrementVersion(CurrentNode, position);
        }
    }
}

