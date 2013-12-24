using System;
using SoftwareMonkeys.csAnt.Packages.Schema;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class InstallCreator
    {
        public IInstallInfo Create(string name)
        {
            var installer = new InstallInfo(name);

            return installer;
        }
    }
}

