using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public new ProjectNodeManager Nodes {
            get {
                return (ProjectNodeManager)base.Nodes;
            }
            set { base.Nodes = value; }
        }
    }
}

