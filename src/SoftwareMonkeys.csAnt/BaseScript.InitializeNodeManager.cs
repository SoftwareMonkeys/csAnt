using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void InitializeNodeManager(INodeManager nodeManager)
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Initializing node manager:");
                Console.WriteLine ("  " + nodeManager.GetType().Name);
                Console.WriteLine ("");
            }

            Nodes = nodeManager;
        }
    }
}

