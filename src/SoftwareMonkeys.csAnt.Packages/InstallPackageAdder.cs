using System;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class InstallPackageAdder
    {
        public InstallLoader Loader { get;set; }

        public InstallSaver Saver { get;set; }

        public InstallPackageAdder(
            InstallLoader loader,
            InstallSaver saver
        )
        {
            Loader = loader;
            Saver = saver;
        }

        public void AddPackages(string workingDirectory, string installName, params string[] packageNames)
        {
            var install = Loader.Load (
                workingDirectory,
                installName
            );

            install.Packages.AddRange(packageNames);

            Saver.Save (workingDirectory, install);
        }
    }
}

