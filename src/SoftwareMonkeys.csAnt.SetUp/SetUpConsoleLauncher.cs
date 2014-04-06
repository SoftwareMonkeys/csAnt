using System;
using SoftwareMonkeys.csAnt.Processes;
using System.IO;


namespace SoftwareMonkeys.csAnt.Projects.Tests.Helpers
{
    public class SetUpConsoleLauncher : BaseTestInstallLauncher
    {
        public DotNetProcessStarter Starter { get;set; }

        public SetUpConsoleLauncher ()
        {
            Starter = new DotNetProcessStarter();
        }

        public override void Launch(string projectDirectory)
        {
            var exeFile = projectDirectory
                + Path.DirectorySeparatorChar
                    + "csAnt-SetUp.exe";

            Starter.Start(exeFile);
        }
    }
}

