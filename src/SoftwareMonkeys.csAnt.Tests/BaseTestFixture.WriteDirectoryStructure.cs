using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        // TODO: Remove these functions if not needed
        public void WriteDirectoryStructure (string[] patterns)
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Directory structure...");
                Console.WriteLine ("Working directory:");
                Console.WriteLine (WorkingDirectory);
                Console.WriteLine ("");
            }
            var dir = WorkingDirectory;
            WriteDirectoryStructure(dir);

            if (IsVerbose)
                Console.WriteLine ("");
        }
        
        public void WriteDirectoryStructure(string subDirectory)
        {
            WriteDirectoryStructure(WorkingDirectory, subDirectory);
        }

        public void WriteDirectoryStructure (string baseDirectory, string subDirectory)
        {
            if (IsVerbose) {
                Console.WriteLine (subDirectory.Replace (baseDirectory, ""));
            }

            foreach (string f in Directory.GetFiles (subDirectory)) {
                if (IsVerbose)
                    Console.WriteLine(f.Replace (baseDirectory, ""));
            }

            foreach (string d in Directory.GetDirectories(subDirectory)) {
                WriteDirectoryStructure(d);
            }
        }
    }
}

