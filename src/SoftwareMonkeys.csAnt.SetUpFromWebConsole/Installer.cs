using System;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole
{
    // TODO: Tidy up the code in this class
    public class Installer
    {
        public IFileFinder FileFinder { get;set; }

        public Installer ()
        {
            FileFinder = new FileFinder();
        }

        public void Install(bool overwrite)
        {
            InstallNuget();

            // TODO: Move this to a config file
            var feedPath = "https://www.myget.org/F/csant/";

            // TODO: Move this to a config file
            var outputDir = "lib";

            // TODO: Move the executor to a property
            var nugetExecutor = new NugetExecutor();
            nugetExecutor.Execute(
                "install",
                "csAnt",
                "-OutputDirectory " + outputDir,
                "-Source " + feedPath
            );

            DeployFiles(overwrite);
        }

        public void InstallNuget()
        {
            // TODO: Move the nuget checker to a property
            var nugetChecker = new NugetChecker();
            nugetChecker.CheckNuget();
        }

        public void DeployFiles(bool overwrite)
        {
            DeployGeneralFiles(overwrite);

            DeployLibFiles(overwrite);
        }

        public void DeployGeneralFiles(bool overwrite)
        {
            var files = new string[]{
                "csAnt.sh",
                "csAnt.bat",
                "scripts/*"
            };

            var libDir = Path.Combine(Environment.CurrentDirectory, "lib");

            var directory = GetVersionedLibDir(libDir);

            foreach (var file in FileFinder.FindFiles(directory, files))
            {
                var toFile = file.Replace(directory, Environment.CurrentDirectory);

                if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(toFile));

                if (overwrite || !File.Exists(toFile))
                    File.Copy(file, toFile, overwrite);
            }
        }

        public void DeployLibFiles(bool overwrite)
        {
            var libDir = Path.Combine(Environment.CurrentDirectory, "lib");

            var versionedDir = GetVersionedLibDir(libDir);

            var versiondSubDir = versionedDir
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                + "csAnt";

            var generalDir = Path.Combine(
                libDir,
                "csAnt"
            );

            // Move from "lib/csAnt.1.2.3.4/lib/csAnt/" to just "/lib/csAnt/"
            Directory.Move(versiondSubDir, generalDir);
        }

        public string GetVersionedLibDir(string libDir)
        {
            return new List<DirectoryInfo>(
                new DirectoryInfo(libDir).GetDirectories().OrderByDescending(p => p.CreationTime)
            )[0].FullName;
        }
    }
}

