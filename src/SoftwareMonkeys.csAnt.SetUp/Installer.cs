using System;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Imports;


namespace SoftwareMonkeys.csAnt.SetUp
{
    // TODO: Tidy up the code in this class
    public class Installer : BaseInstaller
    {
        public IFileFinder FileFinder { get;set; }

        public BaseInstallerRetriever Retriever { get;set; }

        public BaseInstallerFileManager FileManager { get;set; }

        public bool Import { get;set; }
        public string ImportPath { get;set; }

        public Importer Importer { get;set; }

        public string PackageName { get;set; }

        public bool Overwrite { get;set; }

        public Version Version = new Version("0.0.0.0");
        
        public Installer (
            BaseInstallerRetriever retriever,
            BaseInstallerFileManager fileManager
        )
        {
            Retriever = retriever;
            FileManager = fileManager;
            FileFinder = new FileFinder();
            Importer = new Importer();
        }

        public Installer (
            BaseInstallerRetriever retriever
        )
        {
            Retriever = retriever;
            FileManager = new InstallerFileManager();
            FileFinder = new FileFinder();
            Importer = new Importer();
        }

        public Installer (string packageName, string feedPath, string destination)
        {
            Retriever = new InstallerNugetRetriever(destination);
            FileManager = new InstallerFileManager();
            FileFinder = new FileFinder();
            Importer = new Importer();
        }

        public Installer (string sourcePath, string destination)
        {
            Retriever = new InstallerNugetRetriever(destination);
            FileManager = new InstallerFileManager();
            FileFinder = new FileFinder();
            Importer = new Importer();
        }
        
        public Installer ()
        {
            Retriever = new InstallerNugetRetriever();
            FileManager = new InstallerFileManager();
            FileFinder = new FileFinder();
            Importer = new Importer();
        }
        
        /*public void Install(string packageName)
        {
            Install(packageName, Environment.CurrentDirectory, false);
        }

        public void Install(string packageName, bool overwrite)
        {
            Install(packageName, Environment.CurrentDirectory, overwrite);
        }

        public void Install(string packageName, string destination)
        {
            Install(packageName, destination, false);
        }

        public void Install(string packageName, string destination, bool forceOverwrite)
        {
            Install(packageName, destination, new Version(0,0,0,0), forceOverwrite);
        }
        
        public void Install(string packageName, Version version, bool forceOverwrite)
        {
            Install(packageName, Environment.CurrentDirectory, version, forceOverwrite);
        }

        public void Install(string packageName, string destination, Version version, bool forceOverwrite)
        {*/

        public override void Install()
        {
            Console.WriteLine("");
            Console.WriteLine("Installing csAnt...");
            Console.WriteLine("");
            Console.WriteLine("Current directory:");
            Console.WriteLine(Environment.CurrentDirectory);

            Retriever.Retrieve();

            FileManager.InstallFiles(
                Environment.CurrentDirectory, // TODO: Make this configurable
                PackageName,
                Version,
                Overwrite
            );

            if (Import)
                ImportFiles();
        }

        public void DeployFiles(Version version, bool forceOverwrite)
        {
            Console.WriteLine("");
            Console.WriteLine("Installing files...");
            Console.WriteLine("");

            var files = new string[]{
                "csAnt.node",
                "csAnt.sh",
                "csAnt.bat",
                "scripts/**",
                "lib/**"
            };

            var libDir = Path.Combine(Environment.CurrentDirectory, "lib");

            var directory = GetcsAntPackageDir(libDir, version);

            foreach (var file in FileFinder.FindFiles(directory, files))
            {
                var toFile = file.Replace(directory, Environment.CurrentDirectory);

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
                }

                Console.WriteLine(toFile.Replace(Environment.CurrentDirectory, ""));

                File.Copy(
                    file,
                    toFile
                    );
            }
        }

        public void BackupFile(string existingFile)
        {
            throw new NotImplementedException();
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

        public void ImportFiles()
        {
            AddCsAntImport();

            var files = new string[]{
                "*.bat",
                "*.sh",
                "*.vbs",
                "scripts/HelloWorld.cs"
            };

            foreach (var file in files)
                Importer.ImportFile("csAnt", file);

        }

        public void AddCsAntImport()
        {
            Console.WriteLine("");
            Console.WriteLine("Adding csAnt import...");
            Console.WriteLine("");
    
            if (!Importer.ImportExists("csAnt"))
            {
                    Importer.AddImport(
                            "csAnt",
                            ImportPath
                    );
            }
        }
    }
}

