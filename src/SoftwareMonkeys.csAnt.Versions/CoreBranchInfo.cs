using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Versions
{
    public class CoreBranchInfo
    {
        private string[] coreBranches = new string[]{"master", "dev"};
        public string[] CoreBranches
        {
            get{
                // Check for core branch list specified in the .node file (if it exists)
                CheckCoreBranchesInNodeFile ();
                return coreBranches;
            }
        }

        public CoreBranchInfo ()
        {
        }

        public bool IsCoreBranch(string branch)
        {
            return Array.IndexOf (CoreBranches, branch) > -1;
        }

        public void CheckCoreBranchesInNodeFile()
        {
            var nodeManager = new FileNodeManager ();

            if (nodeManager.CurrentNode != null
                && nodeManager.CurrentNode.Properties.ContainsKey ("CoreBranches")) {
                coreBranches = nodeManager.CurrentNode.Properties ["CoreBranches"].Split (',');
            }
        }
    }
}

