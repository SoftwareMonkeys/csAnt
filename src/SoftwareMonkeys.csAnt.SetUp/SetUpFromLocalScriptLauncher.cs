using System;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class SetUpFromLocalScriptLauncher : BaseDeploymentSetUpLauncher
    {
        public ProcessStarter Starter { get;set; }

        public SetUpFromLocalScriptLauncher ()
        {
            Starter = new ProcessStarter();
        }

        public override void Launch(string sourceDirectory, string projectDirectory)
        {
            // TODO: Should sourceDirectory be set to Environment.CurrentDirectory?

            Console.WriteLine("Launching setup from local script...");
            Console.WriteLine("");
            Console.WriteLine("Project directory:");
            Console.WriteLine(projectDirectory);

            // TODO: Should the project directory be set to Environment.CurrentDirectory?
            if (SoftwareMonkeys.csAnt.Processes.Platform.IsLinux)
                Starter.Start("sh", "csAnt-setup-local.sh", sourceDirectory);
            else
                Starter.Start("cscript", "csAnt-setup-local.vbs", sourceDirectory);
        }
    }
}

