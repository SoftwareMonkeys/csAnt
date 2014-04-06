using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.Tests.Helpers
{
    public class HelloWorldLauncher
    {
        public ProcessStarter Starter { get;set; }

        public HelloWorldLauncher ()
        {
            Starter = new ProcessStarter();
        }

        public void Launch()
        {
            if (Platform.IsLinux)
                Starter.Start("sh csAnt.sh HelloWorld");
            else
                Starter.Start("csAnt.bat HelloWorld");
        }
    }
}

