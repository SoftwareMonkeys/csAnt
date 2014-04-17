using System;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Imports;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.SourceControl.Git;
using SoftwareMonkeys.csAnt.SetUp.Install.Retrieve;
using SoftwareMonkeys.csAnt.SetUp.Install.Unpack;


namespace SoftwareMonkeys.csAnt.SetUp.Install
{
    // TODO: Tidy up the code in this class
    public class Installer : BaseInstaller
    {
        public IFileFinder FileFinder { get;set; }

        public BaseInstallerRetriever Retriever { get;set; }

        public BaseInstallUnpacker Unpacker { get;set; }

        public bool Import { get;set; }
        public string ImportPath { get;set; }

        public Importer Importer { get;set; }

        public bool Clone { get;set; }
        public string CloneSource { get;set; }

        public Gitter Git = new Gitter();

        public string PackageName = "csAnt";

        public bool Overwrite { get;set; }

        public Version Version = new Version("0.0.0.0");

        // TODO: Remove if not needed
        public ProcessStarter Starter = new ProcessStarter();
        
        public Installer (
            BaseInstallerRetriever retriever,
            BaseInstallUnpacker unpacker
        )
        {
            Retriever = retriever;
            Unpacker = unpacker;
            FileFinder = new FileFinder();
            Importer = new Importer();
        }

        public Installer (
            BaseInstallerRetriever retriever
        )
        {
            Retriever = retriever;
            Unpacker = new InstallUnpacker();
            FileFinder = new FileFinder();
            Importer = new Importer();
        }

        public Installer (string packageName, string feedPath, string destination)
        {
            Retriever = new InstallerNugetPackageRetriever(destination);
            Unpacker = new InstallUnpacker();
            FileFinder = new FileFinder();
            Importer = new Importer();
        }

        public Installer (string sourcePath, string destination)
        {
            Retriever = new InstallerNugetPackageRetriever(destination);
            Unpacker = new InstallUnpacker();
            FileFinder = new FileFinder();
            Importer = new Importer();
        }
        
        public Installer ()
        {
            Retriever = new InstallerNugetPackageRetriever();
            Unpacker = new InstallUnpacker();
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
            Console.WriteLine("");
            Console.WriteLine("Clone:" + Clone.ToString());
            Console.WriteLine("Clone source:" + CloneSource);
            Console.WriteLine("");
            Console.WriteLine("Import:" + Import.ToString());
            Console.WriteLine("Import path:" + ImportPath);

            Retriever.Retrieve();

            Unpacker.Unpack(
                DestinationPath, // TODO: Make this configurable
                PackageName,
                Version,
                Overwrite
            );

            if (Import)
                ImportFiles();

            if (Clone)
                CloneFiles();

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
                "scripts/HelloWorld.cs",
                "scripts/Update.cs"
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

        public void CloneFiles()
        {
            if (!String.IsNullOrEmpty(CloneSource))
                Git.Clone(CloneSource, DestinationPath);
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Error: Failed to clone. No path was specified on the CloneSource property.");
                Console.WriteLine("");
            }
        }
    }
}

