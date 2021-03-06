using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public override string GetTmpDir ()
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Getting temporary directory...");
            }

            var root = GetTmpRoot ();

            var name = Path.GetFileName (CurrentDirectory);

            var path = String.Empty;

            path = root
                + Path.DirectorySeparatorChar
                + TimeStamp
                + Path.DirectorySeparatorChar
                + GroupName
                + Path.DirectorySeparatorChar
                + name;

            if (IsVerbose) {
                Console.WriteLine ("Root:");
                Console.WriteLine (root);
                Console.WriteLine ("Path:");
                Console.WriteLine (path);
                Console.WriteLine ("");
            }

            EnsureDirectoryExists(path);

            return path;
        }
    }
}

