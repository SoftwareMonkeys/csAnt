using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectNodeGetter : FileNodeGetter
    {
        public new ProjectNodeState State
        {
            get { return (ProjectNodeState)base.State; }
            set { base.State = value; }
        }

        public ProjectNodeGetter (IFileNodeState state) : base(state)
        {
        }

        public override FileNode GetCurrentNode ()
        {
            var node = base.GetCurrentNode ();

            if (State.GroupNode != null)
                State.GroupNode.Nodes.Add(node.Name, node);

            return node;
        }

        public FileNode GetGroupNode()
        {
            var node = GetCurrentNode();

            return node.ParentNode;
        }
    }
}

