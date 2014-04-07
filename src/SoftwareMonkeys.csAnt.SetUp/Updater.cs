using System;
namespace SoftwareMonkeys.csAnt.SetUp
{
    public class Updater
    {
        public Installer Installer { get;set; }

        public string PackageName
        {
            get { return Installer.PackageName; }
            set { Installer.PackageName = value; }
        }

        public Version Version
        {
            get { return Installer.Version; }
            set { Installer.Version = value; }
        }

        public bool Import
        {
            get { return Installer.Import; }
            set { Installer.Import = value; }
        }

        public string ImportPath
        {
            get { return Installer.ImportPath; }
            set { Installer.ImportPath = value; }
        }
        
        public Updater()
        {
            Installer = new Installer();
        }

        public Updater(Installer installer)
        {
            Installer = installer;
        }
        
        public Updater(BaseInstallerRetriever retriever)
        {
            Installer = new Installer(retriever);
        }

        public Updater(BaseInstallerRetriever retriever, BaseInstallerFileManager fileManager)
        {
            Installer = new Installer(retriever, fileManager);
        }

        public Updater (string sourceDir, string destinationDir)
        {
            Installer = new Installer(sourceDir, destinationDir);
        }

        public void Update()
        {
            Console.WriteLine("");
            Console.WriteLine("Updating csAnt...");
            Console.WriteLine("");

            Installer.Install();
        }
    }
}

