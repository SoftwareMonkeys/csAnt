using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void ExecuteScriptFile (string scriptFilePath, params string[] arguments)
        {
            var script = ScriptExecutor.Activator.ActivateScriptFromFile(scriptFilePath);

            ExecuteScript(script, arguments);
        }
    }
}

