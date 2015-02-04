using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.SetUp.Deploy.Launch
{
    public class SetUpFromLocalConsoleLauncher : BaseSetUpLauncher
    {
        public DotNetProcessStarter Starter { get;set; }

        public SetUpFromLocalConsoleLauncher ()
        {
            Starter = new DotNetProcessStarter();
        }
        
        public override void Launch(string sourcePath, string installationPath)
        {
            // TODO: Should sourceDirectory be set to Environment.CurrentDirectory?

            Console.WriteLine("Launching setup from local script...");
            Console.WriteLine("");
            Console.WriteLine("Source path:");
            Console.WriteLine(sourcePath);
            Console.WriteLine("");
            Console.WriteLine("Installation path:");
            Console.WriteLine(installationPath);

            Starter.Start(
                "csAnt-SetUpFromLocal.exe",
                sourcePath,
                "-Destination=" + installationPath
                );
        }

        // TODO: Remove if not needed
        public override void Launch(string sourceDirectory)
        {
            throw new NotImplementedException();
            /*// TODO: Should sourceDirectory be set to Environment.CurrentDirectory?

            Console.WriteLine("Launching setup from local script...");
            Console.WriteLine("");
            Console.WriteLine("Source directory:");
            Console.WriteLine(sourceDirectory);
            Console.WriteLine("");
            Console.WriteLine("Project directory:");
            Console.WriteLine(projectDirectory);

            Starter.Start("csAnt-SetUpFromLocal.exe");*/
        }
    }
}

