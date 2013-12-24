using System;

namespace SoftwareMonkeys.csAnt.PackageManager
{
    public class InstallCreator
    {
        public InstallInfo Create(string name)
        {
            var installer = new InstallInfo(name);
        }
    }
}

