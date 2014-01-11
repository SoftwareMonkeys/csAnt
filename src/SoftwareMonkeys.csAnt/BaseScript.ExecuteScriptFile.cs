using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void ExecuteScriptFile (string scriptFilePath)
        {
            var script = ActivateScriptFromFile(scriptFilePath);

            ExecuteScript(script);
        }
    }
}

