using System;
namespace SoftwareMonkeys.csAnt.SetUp.Common
{
    public class Updater
    {
        public Installer Installer { get;set; }

        public Updater ()
        {
            Installer = new Installer();
        }

        public void Update(string releaseName)
        {
			Update(releaseName, false);
        }

        public void Update(string releaseName, bool forceOverwrite)
        {
            Installer.Install(releaseName, forceOverwrite);
        }
    }
}

