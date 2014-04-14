using System;
namespace SoftwareMonkeys.csAnt
{
    public class LibraryLocalZipGetter
    {
        public INodeState NodeState { get;set; }

        public LibraryLocalZipGetter (INodeState nodeState)
        {
            NodeState = nodeState;
        }
        
        public bool GetLocalZipFileAndExtract(string name)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Retrieving local zip file for: " + name);
            Console.WriteLine ("");
            
            var libNode = NodeState.CurrentNode.Nodes["Libraries"].Nodes[name];

            var localZipFile = libNode.Properties["LocalZipFile"];

            var localZipFilePath = GetLatestLocalZipFilePath(localZipFile);

            var subPath = libNode.Properties["SubPath"];
            throw new NotImplementedException();
            /*var zipFilePath = GetZipFilePath(name);

            var destination = GetLibPath(name);

            if (File.Exists(localZipFilePath))
            {
                File.Copy(localZipFilePath, zipFilePath, true);

                Zipper.Unzip(zipFilePath, destination, subPath);

                return true;
            }
            else
                return false;*/
        }

        public string GetLatestLocalZipFilePath(string localZipPath)
        {
            var output = string.Empty;
            throw new NotImplementedException();

            /*// If there's no wildcard being used
            if (localZipPath.IndexOf("*") == -1)
            {
                output = Path.GetFullPath(localZipPath);
            }
            else
            {
                localZipPath = localZipPath.TrimEnd('*');

                localZipPath = Path.GetFullPath(localZipPath);

                Console.WriteLine("Path:");
                Console.WriteLine(localZipPath);

                var dir = localZipPath;

                var files = new DirectoryInfo(dir).GetFiles("*.zip").OrderByDescending(p => p.CreationTime)
                    .ToArray();

                if (files.Length > 0)
                    output = files[0].FullName;

                Console.WriteLine ("File:");
                Console.WriteLine (output);
                
                Console.WriteLine ("");
            }*/

            return output;
        }
    }
}

