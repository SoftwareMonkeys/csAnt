using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public interface INodeManager
    {
        INodeState State { get;set; }

        void CreateNodes();

        // TODO: Remove if not needed
        //FileNode CreateNode();

        FileNode CreateNode(string location);
        FileNode CreateNode(string location, string name);

        void Refresh();
    }
}

