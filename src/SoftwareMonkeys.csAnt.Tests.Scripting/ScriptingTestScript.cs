using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class ScriptingTestScript : BaseTestScript
    {
        public ScriptingTestScript (string scriptName) : base(scriptName)
        {
        }

        public ScriptingTestScript (string scriptName, IScript parentScript) : base(scriptName, parentScript)
        {
        }

        public override bool Run (string[] args)
        {
            throw new System.NotImplementedException ("Functionality for this script should be implemented by overriding this function.");
        }
    }
}

