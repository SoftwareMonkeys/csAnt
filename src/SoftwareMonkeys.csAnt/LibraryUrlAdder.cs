using System;
using SoftwareMonkeys.FileNodes;
using System.IO;


namespace SoftwareMonkeys.csAnt
{
    public class LibraryUrlAdder
    {
        public INodeState NodeState { get;set; }

        public LibraryUrlAdder (INodeState nodeState)
        {
            NodeState = nodeState;
        }
        
        public void Add(string name, string zipFileUrl)
        {
            Add(name, zipFileUrl, String.Empty);
        }

        public void Add (string name, string zipFileUrl, string subPath)
        {
            EnsureLibsNodeExists();

            CreateLibNode(name, zipFileUrl, subPath);
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
        
        protected void CreateLibNode(string name, string url, string subPath)
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
            node.Properties["Url"] = url;
            node.Properties["SubPath"] = subPath;

            node.Save ();

            NodeState.CurrentNode.Nodes["Libraries"].Nodes.Add (name, node);
        }
    }
}

