using System;
using SoftwareMonkeys.csAnt.Packages.Schema;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.IO.Compression;
using SoftwareMonkeys.csAnt.Versions;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class PackageManager : IPackageManager
    {
        public string WorkingDirectory { get; set; }
        
        public PackageInstaller Retriever { get;set; }

        public PackageCreator Creator { get; set; }

        public PackageSaver Saver { get;set; }

        public PackageFileAdder Adder { get; set; }

        public PackageFileRemover Remover { get;set; }

        public PackageFileScanner Scanner { get;set; }

        public PackageLoader Loader { get; set; }

        public IFileFinder FileFinder { get; set; }

        public PackageBuilder Builder { get; set; }

        public IInstallManager Installers { get; set; }

        public IRepositoryManager Repositories { get; set; }

        public PackageSender Sender { get;set; }

        public VersionManager Versions { get;set; }

        public string PackagesDirectory { get; set; }

        public string FullPackagesDirectory
        {
            get
            {
                return Path.GetFullPath(
                    WorkingDirectory
                    + Path.DirectorySeparatorChar
                    + PackagesDirectory.Trim(Path.DirectorySeparatorChar)
                    );
            }

        }
        
        public PackageManager ()
        {
            Construct (
                Environment.CurrentDirectory,
                "pkgs"
            );
        }
        

        public PackageManager (
            string workingDirectory
        )
        {
            Construct (
                workingDirectory,
                "pkgs"
            );
        }

        public PackageManager (
            string workingDirectory,
            string packagesDirectory
        )
        {
            Construct (
                workingDirectory,
                packagesDirectory
            );
        }

        public PackageManager (
            string workingDirectory,
            string packagesDirectory,
            PackageInstaller retriever,
            PackageBuilder builder,
            PackageCreator creator,
            PackageSaver saver,
            PackageLoader loader,
            PackageSender sender,
            PackageFileAdder adder,
            PackageFileRemover remover,
            PackageFileScanner scanner,
            IRepositoryManager repositories,
            IFileFinder fileFinder,
            IInstallManager installers,
            VersionManager versions
            )
        {
            PackagesDirectory = packagesDirectory;

            WorkingDirectory = workingDirectory;

            Retriever = retriever;

            Builder = builder;

            Creator = creator;

            Saver = saver;

            Adder = adder;

            Remover = remover;

            Scanner = scanner;

            Loader = loader;

            Sender = sender;

            FileFinder = fileFinder;

            Installers = installers;

            Repositories = repositories;

            Versions = versions;
        }

        public void Construct (
            string workingDirectory,
            string packagesDirectory
        )
        {
            WorkingDirectory = workingDirectory;

            PackagesDirectory = packagesDirectory;

            var fileNamer = new PackageInfoFileNamer();

            Retriever = new PackageInstaller();

            Creator = new PackageCreator ();

            FileFinder = new FileFinder ();

            Loader = new PackageLoader ();

            Builder = new PackageBuilder (
                Loader,
                new FileZipper (
                    FileFinder,
                    new DirectoryMover()
                ),
                new PackageZipFileNamer()
            );
        

            Saver = new PackageSaver (
                fileNamer
            );

            Adder = new PackageFileAdder (
                FileFinder,
                Loader,
                Saver
            );

            Sender = new PackageSender(
                Loader,
                fileNamer
            );

            Remover = new PackageFileRemover ();

            Scanner = new PackageFileScanner (
                FileFinder
            );

            Repositories = new RepositoryManager ();

            Installers = new InstallManager ();

            Versions = new VersionManager();
        }

        public void Install (string packageName, string externalRepositoryPath)
        {
            throw new NotImplementedException();
            //Retriever.Pull(WorkingDirectory, FullPackagesDirectory, packageName);
        }

        public void Install (string packageName, string groupName, string externalRepositoryPath)
        {
            Retriever.Install(WorkingDirectory, FullPackagesDirectory, packageName, groupName, externalRepositoryPath);
        }

        public IPackageInfo Create (string packageName, string groupName)
        {
            var pkg = Creator.Create(packageName, groupName);

            Saver.Save(FullPackagesDirectory, pkg);

            return pkg;
        }

        public IPackageInfo Create (string packageName, string groupName, params string[] filePatterns)
        {
            var pkg = Creator.Create(packageName, groupName);

            // TODO: Use the Adder
            foreach (var file in Scanner.Scan (FullPackagesDirectory, filePatterns))
            {
                pkg.Files.Add (file);
            }

            Saver.Save(WorkingDirectory, pkg);

            return pkg;
        }

        public void Build (string packageName, string groupName)
        {
            var version = Versions.GetVersion(WorkingDirectory);
            Build (packageName, groupName, version);
        }

        public void Build (string packageName, string groupName, string version)
        {
            Builder.Build (WorkingDirectory, FullPackagesDirectory, packageName, groupName, version);
        }

        public void AddFiles (string packageName, string groupName, params string[] filePatterns)
        {
            Adder.AddTo(WorkingDirectory, FullPackagesDirectory, packageName, groupName, filePatterns);
        }

        public void AddFile (string packageName, string groupName, params string[] filePatterns)
        {
            AddFiles (packageName, groupName, filePatterns);
        }
        
        public void RemoveFiles (string packageName, string groupName, params string[] filePatterns)
        {
            Remover.RemoveFrom(FullPackagesDirectory, packageName, groupName, filePatterns);
        }

        public void RemoveFile (string packageName, string groupName, params string[] filePatterns)
        {
            RemoveFiles (packageName, groupName, filePatterns);
        }

        public void Send(string packageName, string groupName, string externalRepositoryPath)
        {
            Sender.Send(WorkingDirectory, FullPackagesDirectory, packageName, groupName, externalRepositoryPath);
        }

    }
}

