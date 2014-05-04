using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
    public class LibraryOnlineZipGetter
    {
        public IFileNodeState NodeState { get;set; }

        public LibraryOnlineZipGetter (IFileNodeState nodeState)
        {
            NodeState = nodeState;
        }

        public bool DownloadZipAndExtract(string name, bool overwrite)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Downloading zip file for: " + name);
            Console.WriteLine ("");

            var libNode = NodeState.CurrentNode.Nodes["Libraries"].Nodes[name];

            string url = libNode.Properties["Url"];
            string subPath = String.Empty;

            if (libNode.Properties.ContainsKey("SubPath"))
                subPath = libNode.Properties["SubPath"];

            //var zipFilePath = GetZipFilePath(name);

            //var libPath = GetLibPath(name);

            throw new NotImplementedException();
            /*Script.DownloadAndUnzip(
                url,
                zipFilePath,
                libPath,
                subPath,
                overwrite
            );*/

            return true;
        }
    }
}

