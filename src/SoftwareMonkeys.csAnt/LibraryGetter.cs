using System;
using SoftwareMonkeys.FileNodes;
using System.IO;
using SoftwareMonkeys.csAnt.IO.Compression;
using System.Linq;

namespace SoftwareMonkeys.csAnt
{
    public class LibraryGetter
    {
        public INodeState NodeState { get;set; }
        public IFileZipper Zipper { get;set; }
        public LibraryOnlineZipGetter OnlineZipGetter { get;set; }
        public LibraryLocalZipGetter LocalZipGetter { get;set; }
        public LibraryNugetGetter NugetGetter { get;set; }

        public LibraryGetter (
            INodeState nodeState
        )
        {
            NodeState = nodeState;
            Zipper = new FileZipper();
            OnlineZipGetter = new LibraryOnlineZipGetter(nodeState);
            LocalZipGetter = new LibraryLocalZipGetter(nodeState);
            NugetGetter = new LibraryNugetGetter(nodeState);
        }
        
        public void Get (string name, bool force)
        {
            // TODO: See if this function can be simplified and shortened
            Console.WriteLine ("");
            Console.WriteLine ("Retrieving library: " + name);
            Console.WriteLine ("");

            if (NodeState.CurrentNode.Nodes == null
                || !NodeState.CurrentNode.Nodes.ContainsKey ("Libraries")) {
                Console.WriteLine ("No libraries listed.");
            } else {
                if (!AlreadyFound (name) || force) {
                    if (NodeState.CurrentNode.Nodes ["Libraries"].Nodes.ContainsKey (name)) {
                        var libNode = NodeState.CurrentNode.Nodes ["Libraries"].Nodes [name];

                        var successful = false;

                        // First: Look for an ImportScript property
                        if (libNode.Properties.ContainsKey ("ImportScript")) {
                            Console.WriteLine ("");
                            Console.WriteLine ("Getting library via import script:");
                            Console.WriteLine (libNode.Properties ["ImportScript"]);
                            Console.WriteLine ("");

                            successful = GetLibByImportScript (libNode);
                        }

                        // Next (if unsuccessful): Look for a local zip file property
                        if (successful == false
                            && libNode.Properties.ContainsKey ("nuget")) {
                            Console.WriteLine ("");
                            Console.WriteLine ("Getting library from nuget:");
                            Console.WriteLine (libNode.Properties ["nuget"]);
                            Console.WriteLine ("");

                            successful = NugetGetter.Get (name);
                        }

                        // Next (if unsuccessful): Look for a local zip file property
                        if (successful == false
                            && libNode.Properties.ContainsKey ("LocalZipFile")) {
                            Console.WriteLine ("");
                            Console.WriteLine ("Getting library from local zip file:");
                            Console.WriteLine (libNode.Properties ["LocalZipFile"]);
                            Console.WriteLine ("");

                            successful = LocalZipGetter.GetLocalZipFileAndExtract (name);
                        }

                        // Next (if unsuccessful): Look for a URL property
                        if (successful == false
                            && libNode.Properties.ContainsKey ("Url")) {
                            Console.WriteLine ("");
                            Console.WriteLine ("Getting library via URL download:");
                            Console.WriteLine ("Url: " + libNode.Properties ["Url"]);
                            Console.WriteLine ("Sub path: " + libNode.Properties ["SubPath"]);
                            Console.WriteLine ("");

                            successful = OnlineZipGetter.DownloadZipAndExtract (name, force);
                        }

                        if (!successful)
                            Console.WriteLine ("Couldn't determine import method from '" + libNode.Name + "' library node.");
                    } else
                        Console.WriteLine ("Library not found: '" + name + "'. Add it using 'AddLib [name] [url]'.");
                }
                else
                    Console.WriteLine("'" + name + "' lib is already found. Skipping retrieval.");
            }
        }

        public bool AlreadyFound (string name)
        {
            var libDir = Path.GetDirectoryName(NodeState.CurrentNode.FilePath)
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                + name;

            return Directory.GetFiles(libDir).Length > 1
                || Directory.GetDirectories(libDir).Length > 0;
        }


        public string GetZipFilePath(string name)
        {
            var libPath = GetLibPath(name);

            var zipFilePath = libPath
                + Path.DirectorySeparatorChar
                + name
                + ".zip";

            return zipFilePath;
        }

        public string GetLibPath(string name)
        {
            var libsPath = Path.GetDirectoryName(NodeState.CurrentNode.FilePath)
                + Path.DirectorySeparatorChar
                + "lib";
            
            // TODO: Add a date/time stamp in the file name
            return libsPath
                + Path.DirectorySeparatorChar
                + name;
        }

        public bool GetLibByImportScript(FileNode libNode)
        {
            var scriptName = libNode.Properties["ImportScript"];

            throw new NotImplementedException();
            //Script.ExecuteScript(scriptName);

            return true;
        }
    }
}

