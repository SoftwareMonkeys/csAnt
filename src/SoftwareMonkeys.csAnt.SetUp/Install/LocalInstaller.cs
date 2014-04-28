using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.Versions;
using SoftwareMonkeys.csAnt.SetUp.Install.Retrieve;

namespace SoftwareMonkeys.csAnt.SetUp.Install
{
    /// <summary>
    /// The local installer uses the standard install system and copies files into the destination mimicking a package
    /// and then that package is installed as usual. 
    /// </summary>
    public class LocalInstaller : Installer
    {
        public FileFinder Finder { get; set; }

        public string SourcePath { get;set; }

        public VersionManager Versions { get;set; }

        public LocalInstaller (string sourcePath, string destinationPath, string packageName, bool overwrite)
        {
            Console.WriteLine("");
            Console.WriteLine("Creating local installer...");
            Console.WriteLine("");
            Console.WriteLine("Source path:");
            Console.WriteLine(sourcePath);
            Console.WriteLine("");
            Console.WriteLine("Destination path:");
            Console.WriteLine(destinationPath);
            Console.WriteLine("");
            Console.WriteLine("Package name:");
            Console.WriteLine(packageName);
            Console.WriteLine("");

            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            PackageName = packageName;
            Overwrite = overwrite;
            Finder = new FileFinder();
            Versions = new VersionManager();
            Retriever = new LocalInstallRetriever(SourcePath, DestinationPath, PackageName, Overwrite);

        }
        
        public override void Install ()
        {
            Console.WriteLine("");
            Console.WriteLine("Installing from local source...");
            Console.WriteLine("");

            // If no clone source is specified then use the source path as the default
            // IMPORTANT: These must be done during Install and not in constructor, otherwise updated property values don't get used
            if (Clone
                && String.IsNullOrEmpty(CloneSource))
            {
                Console.WriteLine("Using source path as clone source.");
                CloneSource = SourcePath;
            }

            // If no import source path is specified then use the source path as the default
            // IMPORTANT: These must be done during Install and not in constructor, otherwise updated property values don't get used
            if (Import
                && String.IsNullOrEmpty(ImportPath))
            {
                Console.WriteLine("Using source path as import path.");
                ImportPath = SourcePath;
            }

            // Use base functionality with the local retriever
            base.Install();
        }

        public string[] GetFilePatterns(string filePatternsFile)
        {
            if (!File.Exists(filePatternsFile))
                throw new FileNotFoundException("File patterns list file not found at: " + filePatternsFile);

            return File.ReadAllLines(filePatternsFile);
        }
    }
}

