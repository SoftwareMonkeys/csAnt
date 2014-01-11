using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageSender
    {
        public PackageInfoFileNamer FileNamer { get; set; }

        public PackageLoader Loader { get; set; }

        public PackageSender (
            PackageLoader loader,
            PackageInfoFileNamer fileNamer
        )
        {
            FileNamer = fileNamer;
            Loader = loader;
        }

        public void Send (
            string workingDirectory,
            string packagesDirectory,
            string packageName,
            string groupName,
            string repositoryPath
        )
        {
            Console.WriteLine ("Sending '" + packageName + "' package to repository...");
            Console.WriteLine ("Repository:");
            Console.WriteLine (repositoryPath);

            var packageDir = FileNamer.CreateInfoDirectoryPath (packagesDirectory, packageName, groupName);

            var packageInfo = Loader.Load (packagesDirectory, packageName, groupName);

            var destinationPackageDir = repositoryPath
                + Path.DirectorySeparatorChar
                + packageInfo.GroupName
                + Path.DirectorySeparatorChar
                + packageInfo.Name;

            foreach (var file in Directory.GetFiles(packageDir, "*.zip")) {
                var toFile = file.Replace(packageDir, destinationPackageDir);

                if (!File.Exists(toFile))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                        Directory.CreateDirectory(Path.GetDirectoryName(toFile));
                    
                    Console.WriteLine ("From:");
                    Console.WriteLine ("  " + file);
                    Console.WriteLine ("To:");
                    Console.WriteLine ("  " + toFile);

                    File.Copy(file, toFile);
                }
            }
        }
    }
}

