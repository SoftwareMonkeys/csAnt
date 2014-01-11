using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public override void CreateNodes()
        {
            CreateGroupNode();
            CreateProjectNode();
            CreateSourceNode();

            RefreshCurrentNode();
        }
    }
}

