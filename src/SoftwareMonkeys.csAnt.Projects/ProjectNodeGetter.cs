using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectNodeGetter : NodeGetter
    {
        public new ProjectNodeState State
        {
            get { return (ProjectNodeState)base.State; }
            set { base.State = value; }
        }

        public ProjectNodeGetter (INodeState state) : base(state)
        {
        }

        public override FileNode GetCurrentNode ()
        {
            var node = base.GetCurrentNode ();

            if (State.GroupNode != null)
                State.GroupNode.Nodes.Add(node.Name, node);

            return node;
        }

        public FileNode GetGroupNode()
        {
            //if (IsVerbose)
            //{
            //    Console.WriteLine("");
            //    Console.WriteLine("Getting group node...");
            //    Console.WriteLine("");
            //}
            
            // TODO: See if this should be injected via constructor
            var fileNodes = new FileNodeManager(false); // Check if IsVerbose should be configurable

            // Get the group directory (one step up from the project directory)
            string dir = Path.GetFullPath(
                Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + ".."
            );

            // Scan for the group node
            FileNode node = fileNodes.Get(dir, false, false);
            
            if (node == null)
                throw new GroupNodeNotFoundException();

            if (State.CurrentNode != null)
                node.Nodes.Add (State.CurrentNode.Name, State.CurrentNode);
            
            return node;
        }
    }
}

