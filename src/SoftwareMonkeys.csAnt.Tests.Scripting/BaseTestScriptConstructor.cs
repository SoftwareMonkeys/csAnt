using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class BaseTestScriptConstructor : BaseScriptConstructor
    {
        public BaseTestScriptConstructor (IScript script) : base(script)
        {
        }

        public override void Construct (string scriptName, IScript parentScript)
        {
            base.Construct (scriptName, parentScript);

            var script = (ITestScript)Script;

            script.Utilities = new ScriptingTestUtilities(script);
        }
    }
}

