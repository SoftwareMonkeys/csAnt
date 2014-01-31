using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public class NodeState : INodeState
    {
        private FileNode currentNode;
        public FileNode CurrentNode {
            get {
                if (currentNode == null)
                    Refresh ();

                return currentNode;
            }
            set { currentNode = value; }
        }

        public NodeGetter Getter { get; set; }

        public NodeState ()
        {
            Getter = new NodeGetter();
        }

        public void Refresh()
        {
            CurrentNode = Getter.GetCurrentNode();
        }
    }
}

