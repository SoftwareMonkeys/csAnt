using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public string GetOriginalDirectory ()
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Getting the original directory path...");
                Console.WriteLine ("Current directory:");
                Console.WriteLine (CurrentDirectory);
                Console.WriteLine ("");
            
            }

            var name = String.Empty;

            var path = String.Empty;

            if (!CurrentDirectory.Contains (".tmp")) {
                if (IsVerbose) {
                    Console.WriteLine ("Not currently within .tmp directory.");
                }

                path = Path.GetDirectoryName (CurrentNode.FilePath);
                
                if (IsVerbose) {
                    Console.WriteLine ("Current node file path:");
                    Console.WriteLine (CurrentNode.FilePath);
                }
            }
            else {
                if (IsVerbose) {
                    Console.WriteLine ("Currently within .tmp directory.");
                }

                path = CurrentDirectory;

                while (path.Contains(".tmp"))
                {
                    if (IsVerbose) {
                        Console.WriteLine ("Checking path:");
                        Console.WriteLine (path);
                    }
                    if (path.EndsWith(".tmp"))
                    {
                        if (IsVerbose) {
                            Console.WriteLine ("Ends with '.tmp'. Fixing path so it can be used.");
                            Console.WriteLine (path);
                        }
                        name = Path.GetFileNameWithoutExtension(path);
                        
                        if (IsVerbose) {
                            Console.WriteLine ("Name: " + name);
                        }
                    }
                    path = Path.GetDirectoryName(path);
                }

                path = path
                    + Path.DirectorySeparatorChar
                        + name;
            }
            
            if (IsVerbose) {
                Console.WriteLine ("Path:");
                Console.WriteLine (path);
                Console.WriteLine ("");
            }


            return path;
        }
    }
}

