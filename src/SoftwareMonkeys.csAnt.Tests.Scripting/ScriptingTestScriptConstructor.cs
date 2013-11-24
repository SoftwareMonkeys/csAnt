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
            base.Construct (scriptName, parentScript);

            ConstructSummarizer();
        }

        public override void ConstructLifecycle ()
        {
            var script = (ITestScript)Script;

            script.SetUpper = new ScriptingTestScriptSetUpper (script);

            script.TearDowner = new ScriptingTestScriptTearDowner (script);
        }

        public void ConstructSummarizer()
        {
            var s = (ITestScript)Script;

            s.TestSummarizer = new TestSummarizer(Script);
        }
    }
}

