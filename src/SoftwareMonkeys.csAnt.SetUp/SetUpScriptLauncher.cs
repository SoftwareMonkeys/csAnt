using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.Projects.Tests.Helpers
{
    public class SetUpScriptLauncher : BaseTestInstallLauncher
    {
        public ProcessStarter Starter { get;set; }

        public SetUpScriptLauncher ()
        {
            Starter = new ProcessStarter();
        }

        public override void Launch(string projectDirectory)
        {
            // TODO: Should the project directory be set to Environment.CurrentDirectory?
            if (Platform.IsLinux)
                Starter.Start("sh csAnt-setup.sh");
            else
                Starter.Start("cscript csAnt-setup.vbs");
        }
    }
}

