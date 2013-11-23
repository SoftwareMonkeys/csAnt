using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class DummyScriptTearDowner : BaseScriptTearDowner
    {
        public DummyScriptTearDowner(IDummyScript script) : base(script)
        {
        }

        public override void TearDown ()
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
                Console.WriteLine ("Tearing down '" + Script.ScriptName + "' dummy script...");
                Console.WriteLine ("Component: " + GetType ().Name);

                Console.WriteLine ("Script type: " + Script.GetType().Name);
                Console.WriteLine ("");
            
                Console.WriteLine ("Is verbose: " + Script.IsVerbose.ToString ());
                Console.WriteLine ("Current directory: " + Script.CurrentDirectory);
                Console.WriteLine ("Original directory: " + Script.OriginalDirectory);
           
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
        
            }
        }
    }
}

