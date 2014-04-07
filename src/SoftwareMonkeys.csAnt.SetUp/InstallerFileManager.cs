using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using System.Collections.Generic;
using System.Linq;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class InstallerFileManager : BaseInstallerFileManager
    {
        public IFileFinder FileFinder { get;set; }

        /*public string ProjectDirectory { get;set; }

        public string PackageName { get;set; }

        public Version Version { get;set; }

        public bool ForceOverwrite { get;set; }

        public InstallerFileManager (string projectDirectory, string packageName, Version version, bool forceOverwrite)
        {
            ProjectDirectory = projectDirectory;
            PackageName = packageNAme;
            Version = version;
            ForceOverwrite = forceOverwrite;

            FileFinder = new FileFinder();
        }*/
        public InstallerFileManager ()
        {
            FileFinder = new FileFinder();
        }

        public override void InstallFiles (string projectDirectory, string packageName, Version version, bool forceOverwrite)
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
            Console.WriteLine(version.ToString());
            Console.WriteLine("");
            Console.WriteLine("Force overwrite:");
            Console.WriteLine(forceOverwrite);
            Console.WriteLine("");

            var files = new string[]{
                "csAnt.node",
                "csAnt.sh",
                "csAnt.bat",
                "scripts/**",
                "lib/**"
            };

            var libDir = Path.Combine(projectDirectory, "lib");

            var directory = GetcsAntPackageDir(libDir, version);

            var installedFiles = 0;
            var skippedFiles = 0;
            var overwrittenFiles = 0;

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
                    // TODO: Back up this file before deleting
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
            Console.WriteLine("Files overwritten: " + skippedFiles);
            Console.WriteLine();
        }
        
        public string GetcsAntPackageDir(string libDir, Version version)
        {
            var pkgDir = String.Empty;

            if (version > new Version(0,0,0,0))
            {
                pkgDir = libDir
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

            return pkgDir;
        }
    }
}

