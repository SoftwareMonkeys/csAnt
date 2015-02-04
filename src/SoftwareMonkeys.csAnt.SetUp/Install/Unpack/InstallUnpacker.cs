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

        public InstallUnpacker ()
        {
            FileFinder = new FileFinder();
            Backup = new FileBackup();
        }

        public override void Unpack (string installationDirectory, string packageName, Version version, bool forceOverwrite)
        {
            Console.WriteLine("");
            Console.WriteLine("Installing files...");
            Console.WriteLine("");
            Console.WriteLine("Installation directory:");
            Console.WriteLine(installationDirectory);
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

            var files = new string[]{
                "csAnt.sh",
                "csAnt.bat",
                "scripts/**",
                "lib/**",
                "apps/**"
            };

            var libDir = Path.Combine(installationDirectory, "lib");

            var directory = GetPackageDir(libDir, packageName, version);

            var installedFiles = 0;
            var skippedFiles = 0;
            var overwrittenFiles = 0;

            Console.WriteLine("Looking for files in package directory:");
            Console.WriteLine(directory);
            Console.WriteLine("");

            foreach (var file in FileFinder.FindFiles(directory, files))
            {
                var toFile = file.Replace(directory, installationDirectory);

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

                Console.WriteLine(toFile.Replace(installationDirectory, ""));

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
        
        public string GetPackageDir(string libDir, string packageName, Version versionQuery)
        {
            var pkgDir = String.Empty;

            if (versionQuery != null
                && versionQuery > new Version(0,0,0,0))
            {
                pkgDir = GetSpecificPackageDir (libDir, packageName, versionQuery);
            }
            else
            {
                pkgDir = GetLatestPackageDir (libDir, packageName);
            }

            return pkgDir;
        }

        public string GetSpecificPackageDir(string libDir, string packageName, Version versionQuery)
        {
            var pkgDir = PathConverter.ToAbsolute(
                libDir
                + Path.DirectorySeparatorChar
                + packageName
                + "."
                + versionQuery.ToString()
                );

            return pkgDir;
        }

        public string GetLatestPackageDir(string libDir, string packageName)
        {
            Console.WriteLine ("Identifying latest package directory...");
            Console.WriteLine ("Libs dir: " + libDir);
            Console.WriteLine ("Package name: " + packageName);

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

            if (versions.Count > 0) {
                versions.Sort ();

                var latestVersion = versions [versions.Count - 1];

                var pkgDir = PathConverter.ToAbsolute (
                    libDir
                    + Path.DirectorySeparatorChar
                    + packageName
                    + "."
                    + latestVersion.ToString ()
                );
                return pkgDir;
                
            } else
                throw new Exception ("No packages found to install.");
        }

        public void BackupFile(string filePath)
        {
            Backup.Backup(filePath);
        }
    }
}

