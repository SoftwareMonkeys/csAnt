using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Versions;

namespace SoftwareMonkeys.csAnt.SetUp
{
    /// <summary>
    /// The local installer uses the standard install system and copies files into the destination mimicking a package
    /// and then that package is installed as usual. 
    /// </summary>
    public class LocalInstaller : Installer
    {
        public FileFinder Finder { get; set; }

        public string SourcePath { get;set; }

        // TODO: Remove if not needed
        public string PackageName { get;set; }
        
        // TODO: Remove if not needed
        public bool Overwrite { get;set; }
        
        // TODO: Remove if not needed
        public static Version Version = new Version(0,0,0,0);

        public VersionManager Versions { get;set; }

        public LocalInstaller (string sourcePath, string destinationPath, string packageName, bool overwrite)
        {
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

