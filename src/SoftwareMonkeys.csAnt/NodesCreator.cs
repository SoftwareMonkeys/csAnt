using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public class NodesCreator
    {
        public NodeRefresher Refresher { get;set; }

        public INodeState State { get;set; }

        public NodesCreator (INodeState state)
        {
            Refresher = new NodeRefresher(state);
            State = state;
        }

        public virtual void EnsureNodes()
        {
            if (State.CurrentNode == null)
                CreateNode ();
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

            node.Name = name;

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

