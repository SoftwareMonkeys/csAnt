using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Versions
{
    public class VersionManager
    {
        public VersionManager ()
        {
        }

        public string GetVersion(string workingDirectory)
        {
            var scanner = new FileNodeScanner();

            var node = scanner.ScanDirectory(workingDirectory, false, false);

            if (node == null)
                throw new Exception("File node not found in working directory.");

            if (node.Properties.ContainsKey("Version"))
                return node.Properties["Version"];
            else
                throw new VersionNotFoundException();
        }

        public void SetVersion(FileNode currentNode, Version version)
        {
            Console.WriteLine ("");

            Console.WriteLine ("Setting version to: " + version.ToString());
            
            Console.WriteLine ("");

            currentNode.Properties["Version"] = version.ToString();
            currentNode.Save();

            if (!currentNode.Nodes.ContainsKey("Source"))
                throw new Exception("Can't find 'Source' node.");

            foreach (var node in currentNode.Nodes["Source"].Nodes.Values) {
                node.Properties["Version"] = version.ToString();
                node.Save();
            }
        }

        public void IncrementVersion (FileNode currentNode, int position)
        {
            Console.WriteLine ("");
            
            Console.WriteLine ("Incrementing version...");
            Console.WriteLine ("Position: " + position.ToString());
            
            Console.WriteLine ("");

            var versionString = currentNode.Properties ["Version"];

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
           
            SetVersion(currentNode, version);
        }
    }
}

