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
using SoftwareMonkeys.csAnt.Tests;


namespace SoftwareMonkeys.csAnt.SetUp.Install
{
    // TODO: Tidy up the code in this class
    public class Installer : BaseInstaller
    {
        #region Components
        public IFileFinder FileFinder { get;set; }

        public BaseInstallerRetriever Retriever { get;set; }

        public BaseInstallUnpacker Unpacker { get;set; }

        public Gitter Git = new Gitter();

        public ScriptEventRaiser EventRaiser { get;set; }
        #endregion

        public string PackageName = "csAnt";
        public Version Version = new Version("0.0.0.0");
        public string Status = "";

        public bool Import { get;set; }
        public string ImportPath { get;set; }
        public Importer Importer { get;set; }

        public bool Clone { get;set; }
        public string CloneSource { get;set; }

        public bool Overwrite { get;set; }

        public bool Clear { get;set; }
        
        public Installer (
            BaseInstallerRetriever retriever,
            BaseInstallUnpacker unpacker
        )
        {
            Retriever = retriever;
            Unpacker = unpacker;
            FileFinder = new FileFinder();
            Importer = new Importer();
            EventRaiser = new ScriptEventRaiser();
        }

        public Installer (
            BaseInstallerRetriever retriever
        )
        {
            Retriever = retriever;
            Unpacker = new InstallUnpacker();
            FileFinder = new FileFinder();
            Importer = new Importer();
            EventRaiser = new ScriptEventRaiser();
        }

        public Installer (string packageName, string feedPath, string destination)
        {
            Retriever = new InstallerNugetPackageRetriever(destination);
            Unpacker = new InstallUnpacker();
            FileFinder = new FileFinder();
            Importer = new Importer();
            EventRaiser = new ScriptEventRaiser();
        }

        public Installer (string sourcePath, string destination)
        {
            // TODO: Check if sourcePath parameter is needed
            Retriever = new InstallerNugetPackageRetriever(destination);
            Unpacker = new InstallUnpacker();
            FileFinder = new FileFinder();
            Importer = new Importer();
            EventRaiser = new ScriptEventRaiser();
        }
        
        public Installer ()
        {
            Retriever = new InstallerNugetPackageRetriever();
            Unpacker = new InstallUnpacker();
            FileFinder = new FileFinder();
            Importer = new Importer();
            EventRaiser = new ScriptEventRaiser();
        }

        public override void Install()
        {
            Console.WriteLine("");
            Console.WriteLine("Installing csAnt...");
            Console.WriteLine("");
            Console.WriteLine("Destination:");
            Console.WriteLine("  " + DestinationPath);
            Console.WriteLine("");
            Console.WriteLine("Clear: " + Clear.ToString());
            Console.WriteLine("Overwrite: " + Overwrite.ToString());
            Console.WriteLine("");
            Console.WriteLine("Clone: " + Clone.ToString());
            Console.WriteLine("Clone source:");
            Console.WriteLine("  " + CloneSource);
            Console.WriteLine("");
            Console.WriteLine("Import: " + Import.ToString());
            Console.WriteLine("Import path: ");
            Console.WriteLine("  " + ImportPath);

            if (Clear)
                ClearFiles();

            Retriever.Retrieve(PackageName, Version, Status);

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

        public void ClearFiles()
        {
            Console.WriteLine("");
            Console.WriteLine("Clearing existing files before install...");
            Console.WriteLine("");
            
            // TODO: Check whether this is using the right patterns
            var patterns = DefaultFiles.DefaultFilePatterns;

            foreach (var file in FileFinder.FindFiles(DestinationPath, patterns))
            {
                File.Delete(file);
                Console.WriteLine("  " + file.Replace(DestinationPath, ""));
            }
            Console.WriteLine("");
        }

        public void RaiseInstallEvent()
        {
            // Launch the RaiseEvent script via the standard launcher. This ensures all newly installed assemblies are picked up.
            new ScriptLauncher().Launch("RaiseEvent", "Install"); // TODO: Move to property

            // The following approach results in an error, when trying to load the corresponding scripts
            // new ScriptEventRaiser().Raise("Install");
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

