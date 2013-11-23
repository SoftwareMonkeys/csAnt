using System;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Scripting
{
    public class ProjectTestScriptConstructor : BaseTestScriptConstructor
    {
        public ProjectTestScriptConstructor (IScript script) : base(script)
        {
        }

        public override void Construct (string scriptName, IScript parentScript)
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
                Console.WriteLine ("Constructing '" + scriptName + "' script...");
                Console.WriteLine ("Script type: '" + Script.GetType ().Name + "'");
                Console.WriteLine ("Component: " + GetType ().Name);
                Console.WriteLine ("");
            }

            base.Construct (scriptName, parentScript);


            var script = (ITestScript)Script;

            script.TestSummarizer = new TestSummarizer (script);

            script.SetUpper = new ProjectTestScriptSetUpper (script);

            script.TearDowner = new ProjectTestScriptTearDowner (script);
            
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
            }
        }
    }
}

