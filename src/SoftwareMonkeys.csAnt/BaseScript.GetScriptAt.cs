using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public IScript GetScriptAt(string scriptName, string workingDirectory)
        {
            var script = ScriptExecutor.Activator.ActivateScriptAt(scriptName, workingDirectory);

            return script;
        }
    }
}

