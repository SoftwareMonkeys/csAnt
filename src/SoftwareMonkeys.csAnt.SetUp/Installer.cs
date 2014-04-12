using System;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Imports;
using SoftwareMonkeys.csAnt.Processes;


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

        public string PackageName = "csAnt";

        public bool Overwrite { get;set; }

        public Version Version = new Version("0.0.0.0");

        // TODO: Remove if not needed
        public ProcessStarter Starter = new ProcessStarter();
        
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

            RaiseInstallEvent();
        }

        public void RaiseInstallEvent()
        {
            // TODO: Move event raiser to property
            new ScriptEventRaiser().Raise("Install");

            // TODO: Clean up
            // Launch the install event via a process and the launcher script. Doing it directly from the installer using the ScriptEventRaiser doesn't seem to work.
            //Starter.Start(
            //    "sh",
            //    "csAnt.sh",
            //    "RaiseEvent",
            //    "Install"
            //);
			// TODO: Add support for windows by calling the csAnt.bat file
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
                    new DirectoryInfo(libDir).GetDirectories("csAnt.*").OrderByDescending(p => p.Name)
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
            }; // TODO: Add more

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

