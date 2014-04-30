using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.External.Nuget
{
    public class NugetPackageSpecFileNamer
    {
        public NugetPackageSpecFileNamer ()
        {
        }

        public string GetSpecFileName(string currentDirectory, string packageName)
        {
            return currentDirectory
                + Path.DirectorySeparatorChar
                    + "pkg"
                    + Path.DirectorySeparatorChar
                    + packageName
                    + ".nuspec";
        }
    }
}

