using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public IScript ActivateScriptAt(string scriptName, string workingDirectory)
        {
            var scriptPath = GetScriptPath(scriptName, workingDirectory);

            var script = ActivateScriptFromFile(scriptPath);

            script.Relocate(workingDirectory);

            return script;
        }
    }
}

