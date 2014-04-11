using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public class NodeGetter : INodeGetter
    {
        public INodeState State { get;set; }

        public bool IsVerbose = false;

        public NodeGetter (INodeState state)
        {
            State = state;
        }

        public virtual FileNode GetNode(string relativePath)
        {
            var parts = relativePath.Replace("\"", "/").Trim('/').Split('/');

            FileNode node = null;

            node = State.CurrentNode;

            foreach (string part in parts)
            {
                if (node.Nodes.ContainsKey(part))
                    node = node.Nodes[part];
            }

            return node;
        }
        
        public virtual FileNode GetCurrentNode ()
        {
            if (IsVerbose)
            {
                Console.WriteLine ("");
                Console.WriteLine ("Getting current node...");
                Console.WriteLine ("");
            }

            // TODO: See if this should be injected via constructor
            var fileNodes = new FileNodeManager ();

            string dir = Environment.CurrentDirectory;

            if (IsVerbose)
            {
                Console.WriteLine ("");
                Console.WriteLine ("Current node directory");
                Console.WriteLine (dir);
                Console.WriteLine ("");
            }

            bool foundPropertiesFile = Directory.GetFiles (dir, "*.node").Length > 0;
            
            // Step up the directories looking for .node file
            while (!foundPropertiesFile
                   || dir.IndexOf(Path.DirectorySeparatorChar) == dir.LastIndexOf(Path.DirectorySeparatorChar)) {
                dir = Path.GetDirectoryName (dir);
                
                var nodeCount = Directory.GetFiles (dir, "*.node").Length;
                
                foundPropertiesFile = nodeCount > 0;
            }

            FileNode node = fileNodes.Get (dir, false, true);

            return node;
        }
    }
}

