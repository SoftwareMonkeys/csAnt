using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Versions;

namespace SoftwareMonkeys.csAnt.SetUp
{
    public class LocalInstaller : Installer
    {
        public FileFinder Finder { get; set; }

        public string SourcePath { get;set; }

        public string DestinationPath { get;set; }

        public string PackageName { get;set; }

        public bool Overwrite { get;set; }

        public string[] FilePatterns { get;set; }

        public VersionManager Versions { get;set; }
        
        public LocalInstaller (string sourcePath, string destinationPath, string packageName, bool overwrite)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            PackageName = packageName;
            Overwrite = overwrite;
            FilePatterns = DefaultFiles.DefaultFilePatterns;
            Finder = new FileFinder();
            Versions = new VersionManager();
            Retriever = new LocalInstallRetriever(SourcePath, DestinationPath, PackageName, FilePatterns, Overwrite);
        }

        public LocalInstaller (string sourcePath, string destinationPath, string packageName, string[] filePatterns, bool overwrite)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            PackageName = packageName;
            Overwrite = overwrite;
            FilePatterns = filePatterns;
            Finder = new FileFinder();
            Versions = new VersionManager();
            Retriever = new LocalInstallRetriever(SourcePath, DestinationPath, PackageName, FilePatterns, Overwrite);
        }

        public LocalInstaller (string sourcePath, string destinationPath, string packageName, string filePatternsFile, bool overwrite)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            PackageName = packageName;
            Overwrite = overwrite;
            FilePatterns = GetFilePatterns(filePatternsFile);
            Finder = new FileFinder();
            Versions = new VersionManager();
            Retriever = new LocalInstallRetriever(SourcePath, DestinationPath, PackageName, FilePatterns, Overwrite);
        }
        
        public override void Install ()
        {
            // TODO: Use base functionality with the local retriever
            base.Install();

            /*if (!Directory.Exists (destinationDir))
                Directory.CreateDirectory (destinationDir);

            var files = Finder.FindFiles(sourceDir, patternList);

            Console.WriteLine ("Total files: " + files.Length);

            foreach (var file in files) {
                var toFile = file.Replace (sourceDir, destinationDir);
            
                if (!Directory.Exists (Path.GetDirectoryName (toFile)))
                    Directory.CreateDirectory (Path.GetDirectoryName (toFile));

                Console.WriteLine ("  Copying:");
                Console.WriteLine ("    " + file);
                Console.WriteLine ("  To:");
                Console.WriteLine ("    " + toFile);

                if (overwrite || !File.Exists (toFile))
                    File.Copy (file, toFile);
                else
                    Console.WriteLine ("  Skipping copy.");
            }*/
        }

        public string[] GetFilePatterns(string filePatternsFile)
        {
            if (!File.Exists(filePatternsFile))
                throw new FileNotFoundException("File patterns list file not found at: " + filePatternsFile);

            return File.ReadAllLines(filePatternsFile);
        }
    }
}

