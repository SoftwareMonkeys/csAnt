using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void CreateGroupNode ()
        {
            var path = Path.GetFullPath(CurrentDirectory + "/..");

            CreateGroupNode(path);
        }
        
        public void CreateGroupNode (string path)
        {
            var name = Path.GetFileNameWithoutExtension(path);
            CreateGroupNode(path, name);
        }

        public void CreateGroupNode (string location, string groupName)
        {
            CreateNode(location, groupName);

            // TODO: Remove if not needed
            //throw new NotImplementedException();

            /*if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Creating project node...");
                Console.WriteLine ("Location:");
                Console.WriteLine (location);
                Console.WriteLine ("Group:");
                Console.WriteLine (projectName);
            }

            var groupPath = location;

            var path = groupPath
                + Path.DirectorySeparatorChar
                + projectName
                + ".node";

            var node = new FileNode (
                path,
                new FileNodeSaver ()
            );

            if (!File.Exists (path)) {
                node.Save ();

                GroupNode = node;
            }

            if (IsVerbose) {
                Console.WriteLine ("Path:");
                Console.WriteLine (path);
                Console.WriteLine ("");
            }*/
        }
    }
}

