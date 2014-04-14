using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class SetUpScriptLauncher : BaseDeploymentSetUpLauncher
    {
        public ProcessStarter Starter { get;set; }

        public SetUpScriptLauncher ()
        {
            Starter = new ProcessStarter();
        }

        public override void Launch(string projectDirectory)
        {
            Environment.CurrentDirectory = projectDirectory;

            // TODO: Should the project directory be set to Environment.CurrentDirectory?
            if (Platform.IsLinux)
                Starter.Start("sh", "csAnt-setup.sh");
            else
                Starter.Start("cscript", "csAnt-setup.vbs");
        }

        public override void Launch(string sourceDirectory, string projectDirectory)
        {
            Environment.CurrentDirectory = projectDirectory;

            // TODO: Should the project directory be set to Environment.CurrentDirectory?
            if (Platform.IsLinux)
                Starter.Start("sh", "csAnt-setup.sh", sourceDirectory);
            else
                Starter.Start("cscript", "csAnt-setup.vbs", sourceDirectory);
        }
    }
}

