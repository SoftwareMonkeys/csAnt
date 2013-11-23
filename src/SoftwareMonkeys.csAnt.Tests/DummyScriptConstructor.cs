using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class DummyScriptConstructor : BaseScriptConstructor
    {
        public DummyScriptConstructor (IDummyScript script) : base(script)
        {
        }

        public override void Construct(string scriptName, IScript parentScript)
        {
            base.Construct(scriptName, parentScript);

            Script.FilesGrabber = new FilesGrabber((IDummyScript)Script);

            Script.SetUpper = new DummyScriptSetUpper((IDummyScript)Script);

            Script.TearDowner = new DummyScriptTearDowner((IDummyScript)Script);
        }
    }
}

