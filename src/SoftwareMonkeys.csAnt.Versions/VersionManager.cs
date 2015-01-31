 using System;
using SoftwareMonkeys.FileNodes;
using NuGet;

namespace SoftwareMonkeys.csAnt.Versions
{
    public class VersionManager
    {
        public FileNode CurrentNode { get; set; }
        public FileNodeGetter Getter { get; set; }

        public VersionManager ()
        {
            Construct ();

            Getter.IncludeChildNodes = true;
        }

        public VersionManager (FileNode currentNode)
        {
            Construct ();

            Getter.IncludeChildNodes = true;
            CurrentNode = CurrentNode;
        }

        public void Construct()
        {
            Getter = new FileNodeGetter();
        }

        public string GetVersion(string workingDirectory)
        {
            if (CurrentNode == null)
                CurrentNode = Getter.FindNode(workingDirectory);

            if (CurrentNode == null)
                throw new Exception("File node not found in working directory: " + workingDirectory);

            if (CurrentNode.Properties.ContainsKey("Version"))
                return CurrentNode.Properties["Version"];
            else
                return "0.0.0.0";
        }

        public void SetVersion(FileNode currentNode, Version version)
        {
            Console.WriteLine ("");

            Console.WriteLine ("Setting version to: " + version.ToString());
            
            Console.WriteLine ("");

            currentNode.Properties["Version"] = version.ToString();
            currentNode.Save();

            if (!currentNode.Nodes.ContainsKey("Source"))
                Getter.SetChildNodes(currentNode);

            if (!currentNode.Nodes.ContainsKey("Source"))
                throw new Exception("Can't find 'Source' node (either the file doesn't exist or it wasn't loaded).");

            currentNode.Nodes["Source"].Properties["Version"] = version.ToString();
            currentNode.Nodes["Source"].Save();

            foreach (var node in currentNode.Nodes["Source"].Nodes.Values) {
                node.Properties["Version"] = version.ToString();
                node.Save();
            }
        }

        public void IncrementVersion (FileNode currentNode, int position)
        {
            if (currentNode == null)
                throw new ArgumentNullException("currentNode");

            Console.WriteLine ("");
            
            Console.WriteLine ("Incrementing version...");
            Console.WriteLine ("Position: " + position.ToString());
            
            Console.WriteLine ("");

            var versionString = "";

            if (!currentNode.Properties.ContainsKey("Version"))
                versionString = "0.0.0.1";
            else
                versionString = currentNode.Properties ["Version"];

            var version = new Version (versionString);

            var major = version.Major;
            var minor = version.Minor;
            var revision = version.Revision;
            var build = version.Build;

            switch (position) {
            case 1:
                major += 1;
                minor = 0;
                build = 0;
                revision = 0;
                break;
            case 2:
                minor += 1;
                build = 0;
                revision = 0;
                break;
            case 3:
                build += 1;
                revision = 0;
                break;
            case 4:
                revision += 100;
                break;
            }

            version = new Version(major, minor, build, revision);
           
            SetVersion(currentNode, version);
        }
    }
}

