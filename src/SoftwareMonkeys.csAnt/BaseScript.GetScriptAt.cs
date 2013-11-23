using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public IScript GetScriptAt(string scriptName, string workingDirectory)
        {
            var script = ActivateScriptAt(scriptName, workingDirectory);

            return script;
        }
    }
}

