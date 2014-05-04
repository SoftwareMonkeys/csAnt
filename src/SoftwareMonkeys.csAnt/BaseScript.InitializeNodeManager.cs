using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void InitializeNodeManager(IFileNodeManager nodeManager)
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Initializing node manager:");
                Console.WriteLine ("  " + nodeManager.GetType().Name);
                Console.WriteLine ("");
            }

            Nodes = nodeManager;

            // TODO: Should these be set here or keep values provided?
            Nodes.IncludeParentNodes = true;
            Nodes.IncludeChildNodes = true;
        }
    }
}

