using System;
using System.IO;
using SoftwareMonkeys.csAnt.Packages.Schema;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageEvenAdder
    {
        public string WorkingDirectory { get;set; }

        public IFileFinder FileFinder { get;set; }

        public LocalPackageLoader Loader { get; set; }

        public PackageSaver Saver { get;set; }

        public PackageEvenAdder (
            string packagesDirectory,
            IFileFinder fileFinder,
            LocalPackageLoader loader,
            PackageSaver saver
            )
        {
            WorkingDirectory = packagesDirectory;

            FileFinder = fileFinder;

            Saver = saver;

            Loader = loader;
        }

        public void AddTo (string packagesDirectory, string packageName, string eventName, string scriptName)
        {
            var package = Loader.Load (packagesDirectory, packageName);

            if (package == null)
                throw new PackageNotFoundException(packageName);
            
                var eventInfo = new PackageEventInfo(
                    eventName,
                    new PackageScriptInfo(scriptName)
                );
                if (!package.Events.Contains(eventInfo))
                    package.Events.Add (eventInfo);

            Saver.Save (packagesDirectory, package);

        }
    }
}

