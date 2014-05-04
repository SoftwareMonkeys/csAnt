using System;
using SoftwareMonkeys.FileNodes;
using System.IO;


namespace SoftwareMonkeys.csAnt
{
    // TODO: Decide whether this should be moved to External.Nuget assembly
    public class LibraryNugetAdder
    {
        public IFileNodeState NodeState { get;set; }

        public LibraryNugetAdder (IFileNodeState nodeState)
        {
            NodeState = nodeState;
        }

        public void Add(string name, string packageName)
        {
            EnsureLibsNodeExists();

            CreateLibNode(name, packageName);
        }

        // TODO: Move this to a common utility class
        protected void EnsureLibsNodeExists()
        {
            if (!NodeState.CurrentNode.Nodes.ContainsKey("Libraries"))
                CreateLibsNode();
        }
        
        // TODO: Move this to a common utility class
        protected void CreateLibsNode()
        {
            var libNodePath = Path.GetDirectoryName(NodeState.CurrentNode.FilePath)
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                + "Libraries.node";

            // TODO: Check if these should be injected somehow
            var node = new FileNode(
                libNodePath,
                new FileNodeSaver()
            );

            node.Name = "Libraries";

            node.Save ();

            NodeState.CurrentNode.Nodes.Add ("Libraries", node);
        }
        
        protected void CreateLibNode(string name, string packageName)
        {
            var libNodePath = Path.GetDirectoryName(NodeState.CurrentNode.FilePath)
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                + name
                + Path.DirectorySeparatorChar
                + name + ".node";

            // TODO: Check if these should be injected somehow
            var node = new FileNode(
                libNodePath,
                new FileNodeSaver()
            );

            node.Name = name;
            node.Properties["nuget"] = packageName;

            node.Save ();

            NodeState.CurrentNode.Nodes["Libraries"].Nodes.Add (name, node);
        }
    }
}

