using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Versions
{
    public class CoreBranchInfo
    {
        // TODO: Remove if not needed
        //public string WorkingDirectory { get;set; }

        public CoreBranchInfo ()// string workingDirectory) // TODO: Remove if not needed
        {
            // TODO: Remove if not needed
            //WorkingDirectory = workingDirectory;
        }

        public bool IsCoreBranch(string branch)
        {
            var nodeManager = new FileNodeManager ();

            if (nodeManager.CurrentNode == null)
                throw new Exception ("File node not found in: " + Environment.CurrentDirectory);

            if (!nodeManager.CurrentNode.Properties.ContainsKey ("CoreBranches"))
                throw new Exception ("CoreBranches property not found in *.node");

            var coreBranches = nodeManager.CurrentNode.Properties ["CoreBranches"].Split (',');

            return Array.IndexOf (coreBranches, branch) > -1;
        }
    }
}

