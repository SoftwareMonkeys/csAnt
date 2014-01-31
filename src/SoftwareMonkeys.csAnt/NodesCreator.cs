using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public class NodesCreator
    {
        public NodeRefresher Refresher { get;set; }

        public NodesCreator (INodeState state)
        {
            Refresher = new NodeRefresher(state);
        }
        
        public FileNode CreateNode()
        {
            return CreateNode (Environment.CurrentDirectory, Path.GetFileName(Environment.CurrentDirectory));
        }
        
        public FileNode CreateNode (string location)
        {
            return CreateNode (
                location,
                Path.GetFileName(location)
                );
        }

        public FileNode CreateNode(string location, string name)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Creating node...");
            Console.WriteLine ("Location:");
            Console.WriteLine (location);
            Console.WriteLine ("Name: " + name);

            var path = location
                + Path.DirectorySeparatorChar
                + name
                + ".node";

            var node = new FileNode (
                path,
                new FileNodeSaver ()
            );

            if (!File.Exists (path)) {
                node.Save ();
            }

            Console.WriteLine ("Node file:");
            Console.WriteLine (path);
            Console.WriteLine ("");

            return node;
        }

        public virtual void CreateNodes()
        {
            CreateNode();

            Refresher.RefreshCurrentNode();
        }
    }
}

