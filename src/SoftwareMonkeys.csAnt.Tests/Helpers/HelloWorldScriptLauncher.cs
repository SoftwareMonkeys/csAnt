using System;
using SoftwareMonkeys.csAnt.Processes;


namespace SoftwareMonkeys.csAnt.Tests.Helpers
{
    public class HelloWorldScriptLauncher
    {
        public ScriptLauncher Launcher { get;set; }

        public HelloWorldScriptLauncher ()
        {
            Launcher = new ScriptLauncher();
        }

        public void Launch()
        {
            Launcher.Launch("HelloWorld");
        }
    }
}

