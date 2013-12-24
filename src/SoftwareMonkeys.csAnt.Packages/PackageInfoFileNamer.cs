using System;
using SoftwareMonkeys.csAnt.Packages.Schema;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageInfoFileNamer
    {
        public string CreateInfoFilePath(
            string packagesDirectory,
            string packageName,
            string groupName
        )
        {
            var path = CreateInfoDirectoryPath(
                packagesDirectory,
                packageName,
                groupName
                );

            path = path
                + Path.DirectorySeparatorChar
                + packageName
                + ".pkg";

            return path;
        }
        
        public string CreateInfoDirectoryPath(
            string packagesDirectory,
            string packageName,
            string groupName
        )
        {
            var path = packagesDirectory
                    + Path.DirectorySeparatorChar
                    + groupName
                    + Path.DirectorySeparatorChar
                    + packageName;

            return path;
        }
    }
}

