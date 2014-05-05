using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using System.Collections.Generic;
using System.Linq;
using NuGet;
using SoftwareMonkeys.FileNodes;


namespace SoftwareMonkeys.csAnt.SetUp.Install.Unpack
{
    public class InstallUnpacker : BaseInstallUnpacker
    {
        public IFileFinder FileFinder { get;set; }

        public FileBackup Backup { get;set; }

        public FileNodeManager Nodes { get;set; }

        public InstallUnpacker ()
        {
            FileFinder = new FileFinder();
            Backup = new FileBackup();
            Nodes = new FileNodeManager();
        }

        public override void Unpack (string projectDirectory, string packageName, Version version, bool forceOverwrite)
        {
            Console.WriteLine("");
            Console.WriteLine("Installing files...");
            Console.WriteLine("");
            Console.WriteLine("Project directory:");
            Console.WriteLine(projectDirectory);
            Console.WriteLine("");
            Console.WriteLine("Package name:");
            Console.WriteLine(packageName);
            Console.WriteLine("");
            Console.WriteLine("Version:");
            Console.WriteLine(version != null ? version.ToString() : "[Not set]");
            Console.WriteLine("");
            Console.WriteLine("Force overwrite:");
            Console.WriteLine(forceOverwrite);
            Console.WriteLine("");

            InstallNode(projectDirectory, packageName, version);

            var files = new string[]{
                "csAnt.sh",
                "csAnt.bat",
                "scripts/**",
                "lib/**"
            };

            var libDir = Path.Combine(projectDirectory, "lib");

            var directory = GetPackageDir(libDir, packageName, version);

            var installedFiles = 0;
            var skippedFiles = 0;
            var overwrittenFiles = 0;

            Console.WriteLine("Looking for files in package directory:");
            Console.WriteLine(directory);
            Console.WriteLine("");

            foreach (var file in FileFinder.FindFiles(directory, files))
            {
                var toFile = file.Replace(directory, projectDirectory);

                if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(toFile));

                // TODO: Clean up
                //if (update && File.Exists(toFile))
                //    BackupFile(toFile);

                var isNewer = File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile);

                if (
                    File.Exists(toFile)
                    && (forceOverwrite
                        || isNewer)
                    )
                {
                    BackupFile(toFile);

                    File.Delete(toFile);

                    overwrittenFiles++;
                }

                Console.WriteLine(toFile.Replace(projectDirectory, ""));

                if (!File.Exists(toFile))
                {
                    File.Copy(
                        file,
                        toFile
                        );

                    installedFiles++;
                }
                else
                    skippedFiles++;
            }

            Console.WriteLine();
            Console.WriteLine("Files installed: " + installedFiles);
            Console.WriteLine("Files skipped: " + skippedFiles);
            Console.WriteLine("Files overwritten: " + overwrittenFiles);
            Console.WriteLine();
        }

        public void InstallNode(string projectDirectory, string packageName, Version version)
        {
            Nodes.WorkingDirectory = projectDirectory;
            Nodes.EnsureNodes();
        }
        
        public string GetPackageDir(string libDir, string packageName, Version versionQuery)
        {
            var pkgDir = String.Empty;

            if (versionQuery != null
                && versionQuery > new Version(0,0,0,0))
            {
                pkgDir = PathConverter.ToAbsolute(
                    libDir
                    + Path.DirectorySeparatorChar
                    + packageName
                    + "."
                    + versionQuery.ToString()
                );
            }
            else
            {
                var versions = new List<SemanticVersion>();

                foreach (var dir in Directory.GetDirectories(PathConverter.ToAbsolute(libDir), packageName + ".*"))
                {
                    var versionString = Path.GetFileName(dir).Replace(packageName, "");
                    versionString = versionString.Trim('.');

                    if (!String.IsNullOrEmpty(versionString))
                    {
                        var version = new SemanticVersion(versionString);

                        versions.Add(version);
                    }
                }

                versions.Sort();

                var latestVersion = versions[versions.Count-1];

                pkgDir = PathConverter.ToAbsolute(
                    libDir
                    + Path.DirectorySeparatorChar
                    + packageName
                    + "."
                    + latestVersion.ToString()
                );
            }

            return pkgDir;
        }

        public void BackupFile(string filePath)
        {
            Backup.Backup(filePath);
        }
    }
}

