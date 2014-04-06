using System;
namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void ExecuteScriptAt(string workingDirectory, string scriptName, params string[] arguments)
        {
            var dir = CurrentDirectory;
            Relocate(OriginalDirectory);
            ExecuteScript(scriptName, arguments);
            Relocate(dir);
        }
    }
}

