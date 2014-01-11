using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public virtual string GetWorkingDirectory ()
        {
            Console.WriteLine ("----------");
            Console.WriteLine ("");
            Console.WriteLine ("Getting working directory...");
            Console.WriteLine ("");

            if (IsVerbose) {
                Console.WriteLine ("Time stamp: " + TimeStamp);
            }

            var tdc = new TemporaryDirectoryCreator (
                Environment.CurrentDirectory,
                TimeStamp,
                IsVerbose
            );

            var dir = tdc.GetTmpDir ();

            Console.WriteLine ("Working directory:");
            Console.WriteLine (dir);
            Console.WriteLine ("");
            
            Console.WriteLine ("----------");

            return dir;
        }
    }
}

