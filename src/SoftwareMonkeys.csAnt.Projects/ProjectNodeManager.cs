using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectNodeManager : NodeManager
    {
        public new ProjectNodesCreator Creator {
            get { return (ProjectNodesCreator)base.Creator; }
            set { base.Creator = value; }
        }

        public ProjectNodeManager () : base()
        {
            Creator = new ProjectNodesCreator(State);
        }
    }
}

