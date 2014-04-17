using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class SetUpFromLocalConsoleLauncher : BaseSetUpLauncher
    {
        public DotNetProcessStarter Starter { get;set; }

        public SetUpFromLocalConsoleLauncher ()
        {
            Starter = new DotNetProcessStarter();
        }
        
        public override void Launch(string sourceDirectory, string projectDirectory)
        {
            // TODO: Should sourceDirectory be set to Environment.CurrentDirectory?

            Console.WriteLine("Launching setup from local script...");
            Console.WriteLine("");
            Console.WriteLine("Source directory:");
            Console.WriteLine(sourceDirectory);
            Console.WriteLine("");
            Console.WriteLine("Project directory:");
            Console.WriteLine(projectDirectory);

            Starter.Start(
                "csAnt-SetUpFromLocal.exe",
                sourceDirectory,
                "-Destination=" + projectDirectory
                );
        }

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

