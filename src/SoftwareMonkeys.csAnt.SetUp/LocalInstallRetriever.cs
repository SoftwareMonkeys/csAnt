using System;
using System.IO;
using SoftwareMonkeys.csAnt.Versions;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class LocalInstallRetriever : BaseInstallerRetriever
    {
        public string[] PatternList { get;set; }

        public bool Overwrite { get;set; }

        public IFileFinder FileFinder { get;set; }

        public string SourcePath { get;set; }

        public string DestinationPath { get;set; }

        public string PackageName { get;set; }

        public VersionManager Versions { get;set; }

        public LocalInstallRetriever (
            string sourcePath,
            string destinationPath,
            string packageName,
            string[] patternList,
            bool overwrite
            )
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            PatternList = patternList;
            Overwrite = overwrite;
            PatternList = GetDefaultFilePatternList();
            FileFinder = new FileFinder();
            Versions = new VersionManager();
        }

        public override void Retrieve()
        {
            if (!Directory.Exists (DestinationPath))
                Directory.CreateDirectory (DestinationPath);

            var files = FileFinder.FindFiles(SourcePath, PatternList);

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

                if (Overwrite || !File.Exists (toFile))
                    File.Copy (file, toFile);
                else
                    Console.WriteLine ("  Skipping copy.");
            }
        }

        public string[] GetDefaultFilePatternList()
        {
            return new string[]{
                "lib/csAnt/**",
                "lib/cs-script/**",
                "lib/FileNodes/**",
                "lib/NUnit/**",
                "lib/NUnitResults/**",
                "lib/HtmlAgilityPack/Net40/**",
                "lib/SharpZipLib/net-20/**",
                "lib/ILRepack.1.25.0/**",
                "scripts/**",
                "csAnt.sh"
            };
        }
    }
}

