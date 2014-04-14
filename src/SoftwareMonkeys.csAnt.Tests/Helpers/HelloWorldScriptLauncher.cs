using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.Tests.Helpers
{
    public class HelloWorldScriptLauncher
    {
        public ProcessStarter Starter { get;set; }

        public HelloWorldScriptLauncher ()
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

