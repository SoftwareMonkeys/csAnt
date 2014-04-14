using System;

namespace SoftwareMonkeys.csAnt
{
    public class NodeRefresher
    {
        public NodeGetter Getter { get;set; }

        public INodeState State { get;set; }

        public NodeRefresher (
            INodeState state
        )
        {
            Getter = new NodeGetter(state);
            State = state;
        }

        public void RefreshCurrentNode()
        {
            State.CurrentNode = Getter.GetCurrentNode();
        }
    }
}

