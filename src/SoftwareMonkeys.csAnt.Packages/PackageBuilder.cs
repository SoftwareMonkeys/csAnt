using System;
using System.IO;
using SoftwareMonkeys.csAnt.Versions;
using SoftwareMonkeys.FileNodes;
using SoftwareMonkeys.csAnt.IO.Compression;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageBuilder
    {
        // TODO: Clean up unneeded code
        public PackageLoader Loader { get; set; }

        public IFileZipper Zipper { get; set; }

        public PackageZipFileNamer FileNamer { get; set; }

        public PackageBuilder (
            PackageLoader loader,
            IFileZipper fileZipper,
            PackageZipFileNamer fileNamer
        )
        {
            Loader = loader;

            Zipper = fileZipper;

            FileNamer = fileNamer;
        }

        public string Build (
            string workingDirectory,
            string packagesDirectory,
            string packageName,
            string groupName,
            string version
        )
        {
            Console.WriteLine ("Building '" + packageName + "' package...");

            var package = Loader.Load (packagesDirectory, packageName, groupName);

            if (package == null)
                throw new PackageNotFoundException (package.Name);

            Console.WriteLine ("Version: " + version);

            var zipFilePath = FileNamer.CreateProjectZipFilePath (packagesDirectory, packageName, package.GroupName, version);

            var files = new List<string> ();

            var installScriptFile = Path.GetDirectoryName (zipFilePath)
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + "InstallPackage.cs";

            Console.WriteLine ("Install file:");
            Console.WriteLine (installScriptFile);

            if (File.Exists (installScriptFile)) {
                Console.WriteLine ("Install file found.");
                files.Add (installScriptFile.Replace (workingDirectory, ""));
            }
            else
                Console.WriteLine ("Install file not found.");

            if (package.Files != null) {
                files.AddRange(package.Files.ToStringArray ());

                Console.WriteLine ("Total files: " + files.Count.ToString ());
            }

            Zipper.Zip(
                workingDirectory,
                zipFilePath,
                files.ToArray()
            );

            return zipFilePath;
        }

    }
}

