using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class ScriptingDummyScriptConstructor : DummyScriptConstructor
    {
        public ScriptingDummyScriptConstructor(IDummyScript script) : base(script)
        {}

        // TODO: Check if needed
        /*public override void Construct(string scriptName, IScript parentScript)
        {
            var script = (IScriptingDummyScript)s;

            base.Construct(s);
        }*/
    }
}

