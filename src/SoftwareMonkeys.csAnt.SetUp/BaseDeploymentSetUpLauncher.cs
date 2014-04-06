using System;
namespace SoftwareMonkeys.csAnt.Projects.Tests.Helpers
{
    abstract public class BaseTestInstallLauncher
    {
        public BaseTestInstallLauncher ()
        {
        }

        public abstract void Launch(string projectDirectory);
    }
}

