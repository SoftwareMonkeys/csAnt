using System;

namespace SoftwareMonkeys.csAnt
{
    public class LibraryOnlineZipGetter
    {
        public INodeState NodeState { get;set; }

        public LibraryOnlineZipGetter (INodeState nodeState)
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

