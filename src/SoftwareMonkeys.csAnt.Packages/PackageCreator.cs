using System;
using SoftwareMonkeys.csAnt.Packages.Schema;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageCreator
    {
        public PackageCreator ()
        {
        }

        public PackageInfo Create(string name, string groupName)
        {
            var pkg = new PackageInfo(name, groupName);

            return pkg;
        }
    }
}

