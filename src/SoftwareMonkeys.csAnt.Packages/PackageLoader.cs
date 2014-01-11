using System;
using SoftwareMonkeys.csAnt.Packages.Schema;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageLoader
    {
        public LocalPackageLoader LocalLoader { get;set; }
        
        public PackageLoader (
            LocalPackageLoader localLoader
        )
        {
            LocalLoader = localLoader;
        }

        public PackageLoader ()
        {
            LocalLoader = new LocalPackageLoader();
        }

        public PackageInfo Load(string packagesDirectory, string packageName, string groupName)
        {
            return LocalLoader.Load(packagesDirectory, packageName, groupName);
        }
    }
}

