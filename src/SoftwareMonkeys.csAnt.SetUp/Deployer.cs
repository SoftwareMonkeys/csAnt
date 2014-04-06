using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.Projects.Tests.Helpers
{
    public class Deployer
    {
        public BaseTestInstallRetriever Retriever { get;set; }

        public BaseTestInstallLauncher SetupLauncher { get;set; }

        public Deployer()
        {
            Construct(
                new LocalGitRetriever(), // TODO: Change default retriever to one which directly copies files (to avoid the need to use git)
                new SetUpConsoleLauncher() // TODO: Change default launcher to one which directly uses the Installer component (to avoid the need to build and repack the setup console exe)
                );
        }

        public Deployer (
            BaseTestInstallRetriever retriever,
            BaseTestInstallLauncher launcher
        )
        {
            Construct(
                retriever,
                launcher
                );
        }

        public void Construct (
            BaseTestInstallRetriever retriever,
            BaseTestInstallLauncher launcher
        )
        {
            Retriever = retriever;
            SetupLauncher = launcher;
        }

        public void Install(string source, string destination)
        {
            Retriever.Retrieve(source, destination);

            SetupLauncher.Launch(destination);
        }
    }
}

