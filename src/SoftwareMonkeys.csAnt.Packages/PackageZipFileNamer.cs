using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageZipFileNamer
    {
        public PackageZipFileNamer ()
        {
        }

        public string CreateProjectZipFilePath(string packagesDirectory, string packageName, string groupName, string version)
        {
            return packagesDirectory
                + Path.DirectorySeparatorChar
                + groupName
                + Path.DirectorySeparatorChar
                + packageName
                + Path.DirectorySeparatorChar
                + packageName
                + "-"
                + version.Replace(".", "-")
                + ".zip";
        }
    }
}

