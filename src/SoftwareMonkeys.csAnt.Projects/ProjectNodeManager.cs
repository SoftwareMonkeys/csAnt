using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectNodeManager : NodeManager
    {
        public new ProjectNodeCreator Creator {
            get { return (ProjectNodeCreator)base.Creator; }
            set { base.Creator = value; }
        }
        
        public new ProjectNodeState State{
            get { return (ProjectNodeState)base.State; }
            set { base.State = value; }
        }

        public ProjectNodeManager () : base()
        {
            State = new ProjectNodeState();
            Creator = new ProjectNodeCreator(State);
        }
    }
}

