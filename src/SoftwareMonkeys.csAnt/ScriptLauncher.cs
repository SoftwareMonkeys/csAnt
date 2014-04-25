using System;
using SoftwareMonkeys.csAnt.Processes;

namespace SoftwareMonkeys.csAnt
{
    /// <summary>
    /// Launches scripts via the standard launcher file, which executes the csAnt.exe file and starts the script.
    /// Linux: sh csAnt.sh [ScriptName]
    /// Windows: csAnt.bat [ScriptName]
    /// </summary>
    public class ScriptLauncher
    {
        public ProcessStarter Starter { get;set; }

        public ScriptLauncher ()
        {
            Starter = new ProcessStarter();
        }

        public void Launch(string scriptName, params string[] arguments)
        {
            if (Platform.IsLinux)
                Starter.Start("sh", "csAnt.sh", "RaiseEvent", "Install");
            else
                Starter.Start("csAnt.bat", "RaiseEvent", "Install");
        }
    }
}

