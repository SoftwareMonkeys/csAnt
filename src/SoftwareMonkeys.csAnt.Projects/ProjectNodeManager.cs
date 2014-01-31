using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectNodesManager : NodeManager
    {
        public new ProjectNodesCreator Creator {
            get { return (ProjectNodesCreator)base.Creator; }
            set { base.Creator = value; }
        }

        public ProjectNodesManager () : base()
        {
            Creator = new ProjectNodesCreator(State);
        }
    }
}

