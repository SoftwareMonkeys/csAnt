using System;
using System.IO;
using SoftwareMonkeys.csAnt.Versions;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Tests;
using System.Xml;
using System.Collections.Generic;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class LocalInstallRetriever : BaseInstallerRetriever
    {
        public bool Overwrite { get;set; }

        public IFileFinder FileFinder { get;set; }

        public string SourcePath { get;set; }

        public string DestinationPath { get;set; }

        public string PackageName = "csAnt";

        public VersionManager Versions { get;set; }

        public LocalInstallRetriever (
            string sourcePath,
            string destinationPath,
            string packageName,
            bool overwrite
            )
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            Overwrite = overwrite;
            FileFinder = new FileFinder();
            Versions = new VersionManager();
        }

        public override void Retrieve()
        {
            if (!Directory.Exists (DestinationPath))
                Directory.CreateDirectory (DestinationPath);

            var patterns = GetPatternsFromPackage();

            var files = FileFinder.FindFiles(SourcePath, patterns);

            Console.WriteLine ("Total files: " + files.Length);

            var destinationSubDir = DestinationPath
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "csAnt" // TODO: Make this configurable
                    + "."
                    + Versions.GetVersion(SourcePath);

            foreach (var file in files) {
                var toFile = file.Replace (SourcePath, destinationSubDir);
            
                if (!Directory.Exists (Path.GetDirectoryName (toFile)))
                    Directory.CreateDirectory (Path.GetDirectoryName (toFile));

                Console.WriteLine ("  Copying:");
                Console.WriteLine ("    " + file);
                Console.WriteLine ("  To:");
                Console.WriteLine ("    " + toFile);

                if (File.Exists(toFile)
                    && Overwrite)
                {
                    // TODO: Backup file
                    File.Delete(toFile);
                }

                if (!File.Exists (toFile))
                    File.Copy (file, toFile);
                else
                    Console.WriteLine ("  Skipping copy.");
            }
        }

        public string[] GetDefaultFilePatternList()
        {
            return DefaultFiles.DefaultFilePatterns;
        }

        public string[] GetPatternsFromPackage()
        {
            var packageSpecFile = Path.Combine(SourcePath, "pkg/" + PackageName + ".nuspec");

            var doc = new XmlDocument();

            doc.Load(packageSpecFile);

            var nodes = doc.SelectNodes("//files/file");

            var list = new List<string>();

            foreach (XmlNode node in nodes)
            {
                list.Add(node.Attributes["src"].Value);
            }

            return list.ToArray();
        }
    }
}

