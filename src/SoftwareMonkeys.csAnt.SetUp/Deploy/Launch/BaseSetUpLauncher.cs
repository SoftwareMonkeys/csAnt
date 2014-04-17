using System;


namespace SoftwareMonkeys.csAnt.SetUp.Launchers
{
    abstract public class BaseSetUpLauncher
    {
        public BaseSetUpLauncher ()
        {
        }
        
        public abstract void Launch(string projectDirectory);

        public abstract void Launch(string sourceDirectory, string projectDirectory);
    }
}

