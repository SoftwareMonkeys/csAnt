using System;
namespace SoftwareMonkeys.csAnt.SetUp.Common
{
    abstract public class BaseInstallerLauncher
    {
        public BaseInstallerLauncher ()
        {
        }

        public abstract void Launch(string projectDirectory);
    }
}

