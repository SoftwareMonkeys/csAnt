using System;


namespace SoftwareMonkeys.csAnt.SetUp
{
    abstract public class BaseDeploymentSetUpLauncher
    {
        public BaseDeploymentSetUpLauncher ()
        {
        }

        public abstract void Launch(string sourceDirectory, string projectDirectory);
    }
}

