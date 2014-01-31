using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public interface INodeState
    {
        FileNode CurrentNode { get;set; }

        void Refresh();
    }
}

