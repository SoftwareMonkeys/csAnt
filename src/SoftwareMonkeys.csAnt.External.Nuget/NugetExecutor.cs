using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetExecutor
    {
        // TODO: Make it possible to put this in a config file
        public string NugetPath = "lib/nuget.exe";

        // TODO: Make it possible to put this in a config file
        public string NugetUrl = "http://nuget.org/nuget.exe";

        public DotNetProcessStarter Starter { get;set; }

        public NugetExecutor ()
        {
            Starter = new DotNetProcessStarter();
        }

        public void Execute(params string[] arguments)
        {
            Starter.Start(
                NugetPath,
                arguments
            );
        }
    }
}

