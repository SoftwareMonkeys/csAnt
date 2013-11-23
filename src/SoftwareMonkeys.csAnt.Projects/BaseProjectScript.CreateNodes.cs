using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void CreateNodes()
        {
            CreateGroupNode();
            CreateProjectNode();

            RefreshCurrentNode();
        }
    }
}

