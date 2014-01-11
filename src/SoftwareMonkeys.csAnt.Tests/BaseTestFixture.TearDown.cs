using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        [TearDown]
        public void TearDown ()
        {
            if (!IsTornDown) {
                if (IsVerbose) {
                    Console.WriteLine ("");
                    Console.WriteLine ("----------------------------------------");
                    Console.WriteLine ("");
                    Console.WriteLine ("Tearing down test fixture.");
                    Console.WriteLine ("Component: " + GetType ().Name);
                    Console.WriteLine ("");
                }

                foreach (var s in Scripts) {
                    if (!s.IsTornDown)
                        s.TearDown ();
                }

                if (IsVerbose) {
                    Console.WriteLine ("");
                    Console.WriteLine ("----------------------------------------");
                    Console.WriteLine ("");
                }
                IsTornDown = true;
            }
        }
    }
}

