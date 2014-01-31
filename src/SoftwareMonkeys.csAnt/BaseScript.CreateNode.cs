using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void CreateNode()
        {
            CreateNode (CurrentDirectory, Path.GetFileName(CurrentDirectory));
        }

        public FileNode CreateNode(string location, string name)
        {
            return Nodes.CreateNode(location, name);
        }
    }
}

