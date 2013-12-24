using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class VersionManager : IcsAntComponent
    {
        public string WorkingDirectory { get; set; }

        public FileNode CurrentNode { get; set; }

        public VersionManager (string workingDirectory, FileNode currentNode)
        {
            WorkingDirectory = workingDirectory;

            CurrentNode = currentNode;
        }

        public void SetVersion(Version version)
        {
            Console.WriteLine ("");

            Console.WriteLine ("Setting version to: " + version.ToString());
            
            Console.WriteLine ("");

            CurrentNode.Properties["Version"] = version.ToString();
            CurrentNode.Save();

            if (!CurrentNode.Nodes.ContainsKey("Source"))
                throw new Exception("Can't find 'Source' node.");

            foreach (var node in CurrentNode.Nodes["Source"].Nodes.Values) {
                node.Properties["Version"] = version.ToString();
                node.Save();
            }
        }

        public void IncrementVersion (int position)
        {
            Console.WriteLine ("");
            
            Console.WriteLine ("Incrementing version...");
            Console.WriteLine ("Position: " + position.ToString());
            
            Console.WriteLine ("");

            var versionString = CurrentNode.Properties ["Version"];

            var version = new Version (versionString);

            var major = version.Major;
            var minor = version.Minor;
            var revision = version.Revision;
            var build = version.Build;

            switch (position) {
            case 1:
                major += 1;
                break;
            case 2:
                minor += 1;
                break;
            case 3:
                build += 1;
                break;
            case 4:
                revision += 1000;
                break;
            }

            version = new Version(major, minor, build, revision);
           
            SetVersion(version);
        }
    }
}

