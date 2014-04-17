using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class Deployer
    {
        public BaseDeploymentFilesRetriever Retriever { get;set; }

        public BaseSetUpLauncher SetupLauncher { get;set; }

        public Deployer()
        {
            Construct(
                new LocalGitDeploymentRetriever(), // TODO: Change default retriever to one which directly copies files (to avoid the need to use git)
                new SetUpConsoleLauncher() // TODO: Change default launcher to one which directly uses the Installer component (to avoid the need to build and repack the setup console exe)
                );
        }

        public Deployer (
            BaseDeploymentFilesRetriever retriever,
            BaseSetUpLauncher launcher
        )
        {
            Construct(
                retriever,
                launcher
                );
        }

        public void Construct (
            BaseDeploymentFilesRetriever retriever,
            BaseSetUpLauncher launcher
        )
        {
            Retriever = retriever;
            SetupLauncher = launcher;
        }

        public void Install(string source, string destination)
        {
            Retriever.Retrieve(source, destination);

            SetupLauncher.Launch(source, destination);
        }
    }
}

