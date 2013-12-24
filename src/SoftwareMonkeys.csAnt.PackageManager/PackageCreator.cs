using System;

namespace SoftwareMonkeys.csAnt.InstallConsole
{
    public class PackageCreator
    {
        public PackageCreator ()
        {
        }

        public PackageInfo Create(string name)
        {
            return new PackageFileInfo(name);
        }
    }
}

