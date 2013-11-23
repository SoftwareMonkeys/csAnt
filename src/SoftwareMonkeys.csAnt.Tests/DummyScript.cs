using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class DummyScript : BaseDummyScript
    {
        public DummyScript (string scriptName) : base(scriptName)
        {

        }

        public DummyScript(string scriptName, IScript parentScript) : base(scriptName, parentScript)
        {
        }

        public override bool Run (string[] args)
        {
            throw new System.NotImplementedException ();
        }
    }
}

