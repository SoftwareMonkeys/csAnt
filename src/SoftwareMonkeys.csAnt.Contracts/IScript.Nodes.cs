using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Nodes
        IFileNodeManager Nodes { get;set; }

        void InitializeNodeManager(IFileNodeManager nodeManager);

        FileNode CurrentNode { get;set; }

        void CreateNodes();
        #endregion
    }
}

