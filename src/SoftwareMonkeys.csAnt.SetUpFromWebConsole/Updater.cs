using System;
namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole
{
    public class Updater
    {
        public Installer Installer { get;set; }

        public Updater ()
        {
            Installer = new Installer();
        }

        public void Update()
        {
			Update(false);
        }

        public void Update(bool forceOverwrite)
        {
            Installer.Install(forceOverwrite);
        }
    }
}

