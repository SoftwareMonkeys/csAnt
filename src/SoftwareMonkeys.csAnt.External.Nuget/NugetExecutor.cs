using System;
using SoftwareMonkeys.csAnt.Processes;
using System.Diagnostics;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetExecutor
    {
        // TODO: Make it possible to put this in a config file
        public string NugetFilePath = "lib/nuget.exe";

        public DotNetProcessStarter Starter { get;set; }

        public Process CurrentProcess { get;set; }

        public NugetExecutor ()
        {
            Starter = new DotNetProcessStarter();
        }

        public virtual void Execute(params string[] arguments)
        {
            CurrentProcess = Starter.Start(
                NugetFilePath,
                arguments
            );
        }
    }
}

