using System;
using SoftwareMonkeys.csAnt.Packages.Schema;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class InstallManager : IInstallManager
    {
        public InstallCreator Creator { get;set; }

        public InstallSaver Saver { get;set; }

        public InstallLoader Loader { get; set; }

        public LocalPackageLoader PackageLoader { get;set; }

        public InstallPackageAdder PackageAdder { get;set; }

        public InstallManager ()
        {
            Construct();
        }

        public InstallManager(
            InstallCreator creator,
            InstallSaver saver,
            InstallLoader loader,
            InstallPackageAdder packageAdder
            )
        {
            Creator = creator;
            Loader = loader;
            Saver = saver;
            PackageAdder = packageAdder;
        }

        public void Construct()
        {
            Creator = new InstallCreator();

            Loader = new InstallLoader();

            Saver = new InstallSaver();
        
            PackageAdder = new InstallPackageAdder(
                Loader,
                Saver
            );
        }

        public IInstallInfo Create(string workingDirectory, string name)
        {
            if (Loader.Load (workingDirectory, name) != null)
                throw new InstallExistsException(name);

            var install = Creator.Create(name);

            Save (workingDirectory, install);

            return install;
        }

        public void Save(string workingDirectory, IInstallInfo install)
        {
            Saver.Save (workingDirectory, install);
        }

        public IInstallInfo Load(string workingDirectory, string name)
        {
            return Loader.Load (workingDirectory, name);
        }

        public void AddPackages(string workingDirectory, string installName, params string[] packageNames)
        {
            PackageAdder.AddPackages(workingDirectory, installName, packageNames);
        }
    }
}

