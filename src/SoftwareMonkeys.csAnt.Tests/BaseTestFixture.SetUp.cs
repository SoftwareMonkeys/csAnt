using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        [SetUp]
        public void SetUp ()
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("----------------------------------------");
                Console.WriteLine ("");
                Console.WriteLine ("Setting up test BaseTestFixture...");
                Console.WriteLine (GetType ().Name);
            }

            TimeStamp = GetTimeStamp ();

            if (IsVerbose) {
                Console.WriteLine ("Time stamp: " + TimeStamp);

                Console.WriteLine ("Auto initialize: " + AutoInitialize.ToString ());
                Console.WriteLine ("");
            }

            if (AutoInitialize) {
                FixCurrentDirectory ();

                WorkingDirectory = GetWorkingDirectory();
                OriginalDirectory = GetOriginalDirectory();

                Nodes.EnsureNodes();
            }
             
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Working directory:");
                Console.WriteLine (WorkingDirectory);
                Console.WriteLine ("");
                Console.WriteLine ("----------------------------------------");
                Console.WriteLine ("");
            }
        }
    }
}

