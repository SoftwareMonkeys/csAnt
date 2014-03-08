using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Nodes
        INodeManager Nodes { get;set; }

        void InitializeNodeManager(INodeManager nodeManager);

        FileNode CurrentNode { get;set; }

        void CreateNodes();
        #endregion
    }
}

