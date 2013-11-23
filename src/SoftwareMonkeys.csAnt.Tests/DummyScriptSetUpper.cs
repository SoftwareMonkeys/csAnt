using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class DummyScriptSetUpper : BaseScriptSetUpper
    {
        public DummyScriptSetUpper (IScript script) : base(script)
        {
        }

        public override void SetUp ()
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
            }

            base.SetUp ();   

            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Setting up '" + Script.ScriptName + "' script...");
                Console.WriteLine ("Script type: " + Script.GetType().Name);
                Console.WriteLine ("Time: " + Script.Time.ToString ());
                Console.WriteLine ("Time stamp: " + Script.TimeStamp);
            }

            Script.OriginalDirectory = Script.GetOriginalDirectory ();

            if (Script.IsVerbose) {
                Console.WriteLine ("Original directory: " + Script.OriginalDirectory);
                     
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
            }
        }
    }
}

