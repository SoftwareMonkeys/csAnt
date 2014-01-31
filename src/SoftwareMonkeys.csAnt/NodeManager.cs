using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public class NodeManager : INodeManager
    {
        public NodeGetter Getter { get; set; }

        public NodesCreator Creator { get; set; }

        public INodeState State { get; set; }

        public NodeManager ()
        {
            Getter = new NodeGetter();
            State = new NodeState();
            Creator = new NodesCreator(State);
        }

        public void CreateNodes()
        {
            Creator.CreateNodes();
        }

        public FileNode CreateNode(string location, string name)
        {
            return Creator.CreateNode (location, name);
        }

        public FileNode CreateNode(string location)
        {
            return Creator.CreateNode (location);
        }

        public void Refresh()
        {
            State.Refresh();
        }
    }
}

