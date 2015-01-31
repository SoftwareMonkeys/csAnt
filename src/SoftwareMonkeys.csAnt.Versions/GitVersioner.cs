using System;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.SourceControl.Git;
using SoftwareMonkeys.FileNodes;
using SoftwareMonkeys.csAnt.IO;
using System.IO;
using NuGet;

namespace SoftwareMonkeys.csAnt.Versions
{
    public class GitVersioner
    {
        public string Branch { get;set; }

        public GitVersioner (string branch)
        {
            Branch = branch;
        }
        
        public SemanticVersion GetOriginVersion()
        {
            var starter = new ProcessStarter ();
            starter.Start ("git config --get remote.origin.url");

            var originPath = starter.Output;
            Console.WriteLine ("Origin path: " + originPath);

            var tmpDir = Path.Combine (Environment.CurrentDirectory, "_originclone");

            new Gitter ().Clone (originPath, Branch, tmpDir);
            
            var nodes = new FileNodeManager (tmpDir);
            nodes.Refresh ();

            var version = nodes.CurrentNode.Properties ["Version"];

            var status = nodes.CurrentNode.Properties ["Status"];

            Directory.Delete (tmpDir, true);

            var versionString = version;
            if (!String.IsNullOrEmpty (status))
                versionString += "-" + status;

            return new SemanticVersion(versionString);
        }
    }
}

