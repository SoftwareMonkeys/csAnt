using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void CompileScript(string scriptName)
        {
            CompileScripts (scriptName);
        }
        
        public void CompileScript(string scriptName, bool force)
        {
            CompileScripts (force, scriptName);
        }
    }
}

