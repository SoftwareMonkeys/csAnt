using System;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUp.Common
{
    // TODO: Tidy up the code in this class
    public class Installer
    {
        public IFileFinder FileFinder { get;set; }

        public string NugetFeedPath { get;set; }

        public NugetChecker NugetChecker { get;set; }

        public NugetExecutor NugetExecutor { get;set; }

        public Installer ()
        {
            FileFinder = new FileFinder();
            NugetFeedPath = "https://www.myget.org/F/csant/";
            NugetChecker = new NugetChecker();
            NugetExecutor = new NugetExecutor();
        }
        
        public void Install(string releaseName)
        {
            Install(releaseName, false);
        }

        public void Install(string releaseName, bool forceOverwrite)
        {
            Install(releaseName, new Version(0,0,0,0), forceOverwrite);
        }

        public void Install(string releaseName, Version version, bool forceOverwrite)
        {
            InstallNuget();

            // TODO: Move this to a config file
            var outputDir = "lib";

            var arguments = new List<string>();
            arguments.Add("install");
            arguments.Add("csAnt");
            arguments.Add("-OutputDirectory " + outputDir);
            arguments.Add("-Source " + NugetFeedPath);

            if (version > new Version(0, 0, 0, 0))
                arguments.Add("-Version " + version.ToString());

            // TODO: Move the executor to a property
            NugetExecutor.Execute(
                arguments.ToArray()
            );

            DeployFiles(version, forceOverwrite);
        }

        public void InstallNuget()
        {
            NugetChecker.CheckNuget();
        }

        public void DeployFiles(Version version, bool forceOverwrite)
        {
            DeployGeneralFiles(version, forceOverwrite);

            DeployLibFiles(version, forceOverwrite);
        }

        public void DeployGeneralFiles(Version version, bool forceOverwrite)
        {
            var files = new string[]{
                "csAnt.sh",
                "csAnt.bat",
                "scripts/*"
            };

            var libDir = Path.Combine(Environment.CurrentDirectory, "lib");

            var directory = GetcsAntPackageDir(libDir, version);

            foreach (var file in FileFinder.FindFiles(directory, files))
            {
                var toFile = file.Replace(directory, Environment.CurrentDirectory);

                if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(toFile));

                //if (update && File.Exists(toFile))
                //    BackupFile(toFile);

                if (
                    forceOverwrite
                    || !File.Exists(toFile)
                    || File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile)
                )
                {
                    File.Copy(file, toFile, forceOverwrite);
                }
            }
        }

        public void BackupFile(string existingFile)
        {

        }

        public void DeployLibFiles(Version version, bool forceOverwrite)
        {
            var libDir = Path.Combine(Environment.CurrentDirectory, "lib");

            var versionedDir = GetcsAntPackageDir(libDir, version);

            var versiondSubDir = versionedDir
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                + "csAnt";

            var generalDir = Path.Combine(
                libDir,
                "csAnt"
            );

            // TODO: Moved component to property
            // Move from "lib/csAnt.1.2.3.4/lib/csAnt/" to just "/lib/csAnt/"
            new DirectoryMover().Move(versiondSubDir, generalDir, forceOverwrite);
        }

        public string GetcsAntPackageDir(string libDir, Version version)
        {
            if (version > new Version(0,0,0,0))
            {
                return libDir
                    + Path.DirectorySeparatorChar
                    + "csAnt."
                        + version.ToString();
            }
            else
            {
                return new List<DirectoryInfo>(
                    new DirectoryInfo(libDir).GetDirectories("csAnt.*").OrderByDescending(p => p.CreationTime)
                )[0].FullName;
            }
        }
    }
}

