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
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Creating node...");
                Console.WriteLine ("Location:");
                Console.WriteLine (location);
                Console.WriteLine ("Name: " + name);
            }

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

            if (IsVerbose) {
                Console.WriteLine ("Node file:");
                Console.WriteLine (path);
                Console.WriteLine ("");
            }

            return node;
        }
    }
}

