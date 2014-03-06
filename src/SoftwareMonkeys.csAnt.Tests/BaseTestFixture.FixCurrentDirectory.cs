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

        	Environment.CurrentDirectory = new CurrentDirectoryFixer().Fix(Environment.CurrentDirectory);
            
            if (IsVerbose) {
                Console.WriteLine ("New current directory:");
                Console.WriteLine (Environment.CurrentDirectory);
                Console.WriteLine ("");
            }
        }
    }
}

