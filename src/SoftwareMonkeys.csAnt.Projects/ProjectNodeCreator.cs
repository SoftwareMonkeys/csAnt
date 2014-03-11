using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectNodesCreator : NodesCreator
    {
        public ProjectNodesCreator (INodeState nodeState) : base(nodeState)
        {
        }

        public override void CreateNodes ()
        {
            CreateProjectNode();
            CreateSourceNode();
            CreateGroupNode();

            Refresher.RefreshCurrentNode();
        }

        public void CreateProjectNode ()
        {
            CreateProjectNode(Environment.CurrentDirectory);
        }

        public void CreateProjectNode (string projectDirectory)
        {
            var name = Path.GetFileNameWithoutExtension(projectDirectory);
            CreateProjectNode(projectDirectory, name);
        }

        public void CreateProjectNode (string currentDirectory, string projectName)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Creating project node...");
            Console.WriteLine ("  Current directory:");
            Console.WriteLine ("  " + currentDirectory);

            var node = CreateNode(currentDirectory, projectName);

            node.Properties["Version"] = new Version(0, 0, 0, 1).ToString();

            Console.WriteLine ("  Version: " + node.Properties["Version"]);
            Console.WriteLine ("");

            if (!File.Exists(node.FilePath))
                node.Save();
        }

        public void CreateSourceNode ()
        {
            CreateSourceNode(Environment.CurrentDirectory);
        }

        public void CreateSourceNode (string currentDirectory)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Creating source node...");
            Console.WriteLine ("  Current directory:");
            Console.WriteLine ("  " + currentDirectory);

            var srcDirectory = currentDirectory
                + Path.DirectorySeparatorChar
                    + "src";

            var node = CreateNode (
                srcDirectory,
                "Source"
                );

            Console.WriteLine ("  Source (src) directory:");
            Console.WriteLine ("  " + srcDirectory);
            
            if (!File.Exists(node.FilePath))
                node.Save();
        }

        public void CreateGroupNode ()
        {
            CreateGroupNode(Environment.CurrentDirectory);
        }
        
        public void CreateGroupNode (string currentDirectory)
        {
            var path = Path.GetFullPath(currentDirectory + "/..");

            CreateGroupNode(path, Path.GetFileName(path));
        }

        public void CreateGroupNode (string groupDirectory, string groupName)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Creating group node...");
            Console.WriteLine ("  Group name: " + groupName);
            Console.WriteLine ("  Group directory:");
            Console.WriteLine ("  " + groupDirectory);

            CreateNode(groupDirectory, groupName);
        }
    }
}

