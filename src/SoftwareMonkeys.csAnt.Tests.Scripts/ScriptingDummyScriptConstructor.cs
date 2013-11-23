using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    public class ScriptingDummyScriptConstructor : DummyScriptConstructor
    {

        public override void Construct(IDummyScript s)
        {
            var script = (IScriptingDummyScript)s;

            base.Construct(s);
        }
    }
}

