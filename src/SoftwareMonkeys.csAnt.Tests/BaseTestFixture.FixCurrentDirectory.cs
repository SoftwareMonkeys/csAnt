using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public void FixCurrentDirectory ()
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Fixing current directory...");
                Console.WriteLine ("Current directory:");
                Console.WriteLine (Environment.CurrentDirectory);
                Console.WriteLine ("");
            }

            var modes = new string[]{"Debug", "Release"};

            foreach (var mode in modes) {
                if (Environment.CurrentDirectory.EndsWith ("/bin/" + mode))
                    Environment.CurrentDirectory = Environment.CurrentDirectory.Replace ("/bin/" + mode, "");
            }
            
            if (IsVerbose) {
                Console.WriteLine ("New current directory:");
                Console.WriteLine (Environment.CurrentDirectory);
                Console.WriteLine ("");
            }
        }
    }
}

