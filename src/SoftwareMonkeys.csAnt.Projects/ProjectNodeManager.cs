using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectNodeManager : FileNodeManager
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

        public ProjectNodeManager (FileNode currentNode) : base()
        {
            IncludeChildNodes = true;
            IncludeParentNodes = true;
            State = new ProjectNodeState();
            Creator = new ProjectNodeCreator(State);
            State.CurrentNode = currentNode;
        }

        public ProjectNodeManager (string workingDirectory) : base(workingDirectory)
        {
            IncludeChildNodes = true;
            IncludeParentNodes = true;
            State = new ProjectNodeState();
            Creator = new ProjectNodeCreator(State);
        }
    }
}

