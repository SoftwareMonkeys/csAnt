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
                    Console.WriteLine ("Tearing down scripting test script...");

                    Console.WriteLine ("Component: " + GetType ().Name);

                    Console.WriteLine ("Script name: " + Script.ScriptName);
                    Console.WriteLine ("");
                }

                var script = (ITestScript)Script;

                if (Script.IsVerbose)
                    Console.WriteLine ("Is verbose: " + Script.IsVerbose.ToString ());

                script.Summarizer.Summarize ();

                script.ReportGenerator.GenerateReports ();

                script.Utilities.CopyTestResults (Script.CurrentDirectory, Script.OriginalDirectory);

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
                    Console.WriteLine ("Already torn down. Skipping tear down.");
            }
        }
    }
}

