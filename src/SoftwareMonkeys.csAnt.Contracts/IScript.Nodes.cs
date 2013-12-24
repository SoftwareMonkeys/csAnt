using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Nodes
        FileNode CurrentNode { get;set; }

        void RefreshCurrentNode();
        #endregion
    }
}

