using System;
using System.IO;
using SoftwareMonkeys.csAnt.Packages.Schema;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageFileAdder
    {
        public IFileFinder FileFinder { get;set; }

        public PackageLoader Loader { get; set; }

        public PackageSaver Saver { get;set; }

        public PackageFileAdder (
            IFileFinder fileFinder,
            PackageLoader loader,
            PackageSaver saver
            )
        {
            FileFinder = fileFinder;

            Saver = saver;

            Loader = loader;
        }

        public void AddTo (string workingDirectory, string packagesDirectory, string packageName, string groupName, params string[] filePatterns)
        {
            var package = Loader.Load (packagesDirectory, packageName, groupName);

            if (package == null)
                throw new PackageNotFoundException(packageName);

            foreach (var file in FileFinder.FindFiles(workingDirectory, filePatterns)) {
                Console.WriteLine (file);
                var fileInfo = new PackageFileInfo(
                    file.Replace (workingDirectory, "").TrimStart(Path.DirectorySeparatorChar)
                );
                if (!package.Files.Contains(fileInfo))
                    package.Files.Add (fileInfo);
            }

            Saver.Save (packagesDirectory, package);

        }
    }
}

