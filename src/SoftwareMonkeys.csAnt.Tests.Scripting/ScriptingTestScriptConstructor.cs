using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class ScriptingTestScriptConstructor : BaseScriptConstructor
    {
        public ScriptingTestScriptConstructor (ITestScript script) : base(script)
        {
        }

        public override void Construct (string scriptName, IScript parentScript)
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
                Console.WriteLine ("Constructing '" + Script.ScriptName + "' script...");
                Console.WriteLine ("Script type: '" + Script.GetType ().Name + "'");
                Console.WriteLine ("Component: " + GetType ().Name);
                Console.WriteLine ("");
            }

            base.Construct (scriptName, parentScript);

            var script = (ITestScript)Script;

            script.Summarizer = new TestSummarizer (script);

            script.ReportGenerator = new ScriptReportGenerator (script);

            script.SetUpper = new ScriptingTestScriptSetUpper (script);

            script.TearDowner = new ScriptingTestScriptTearDowner (script);

            script.Utilities = new ScriptingTestUtilities (script);
            
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
            }
        }
    }
}

