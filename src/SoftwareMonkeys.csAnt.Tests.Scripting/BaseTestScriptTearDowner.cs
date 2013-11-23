using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class BaseTestScriptTearDowner : BaseScriptTearDowner
    {
        public BaseTestScriptTearDowner (IScript script) : base(script)
        {
        }
        
        public override void TearDown ()
        {
            if (!Script.IsTornDown) {
                if (Script.IsVerbose) {
                    Console.WriteLine ("");
                    Console.WriteLine ("--------------------------------------------------");
                    Console.WriteLine ("");
                    Console.WriteLine ("Tearing down '" + Script.ScriptName + "' script...");

                    Console.WriteLine ("Component: " + GetType ().Name);

                    Console.WriteLine ("Script type: " + Script.GetType().Name);
                    Console.WriteLine ("");
                }

                var script = (ITestScript)Script;

                if (Script.IsVerbose)
                    Console.WriteLine ("Is verbose: " + Script.IsVerbose.ToString ());

                script.TestSummarizer.Summarize ();

                if (Script.IsVerbose) {
                    Console.WriteLine ("Current directory: " + Script.CurrentDirectory);
                    Script.CurrentDirectory = Script.OriginalDirectory;
                    Console.WriteLine ("Original directory: " + Script.OriginalDirectory);
                }

                script.Relocator.Return ();

                if (Script.IsVerbose) {
                    Console.WriteLine ("");
                    Console.WriteLine ("--------------------------------------------------");
                    Console.WriteLine ("");
                }
            } else {
                if (Script.IsVerbose)
                {
                    Console.WriteLine ("");
                    Console.WriteLine ("'" + Script.ScriptName + "' script has already been torn down. Not tearing down again.");
                    Console.WriteLine ("");
                }
            }
        }
    }
}

