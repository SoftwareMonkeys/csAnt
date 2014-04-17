using System;
using System.IO;
using SoftwareMonkeys.csAnt.Versions;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Tests;
using System.Xml;
using System.Collections.Generic;


namespace SoftwareMonkeys.csAnt.SetUp
{
    /// <summary>
    /// Retrieves installation files from a local csAnt project using the [PackageName].nuspec file to determine which files to retrieve.
    /// </summary>
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

            var patterns = GetFilePatterns();

            Console.WriteLine("File patterns:");
            foreach (var pattern in patterns)
                Console.WriteLine("  " + pattern);
            Console.WriteLine("");

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
                var toFile = file.Replace (SourcePath.TrimEnd(Path.DirectorySeparatorChar), destinationSubDir.TrimEnd(Path.DirectorySeparatorChar));
            
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

        public string[] GetFilePatterns()
        {
            var packageSpecFile = Path.Combine(SourcePath, "pkg/" + PackageName + ".nuspec");

            var doc = new XmlDocument();

            doc.Load(packageSpecFile);

            var list = new List<string>();
            
            list.AddRange(GetFilePatternsFromNugetSpecFiles(doc));
            list.AddRange(GetFilePatternsFromNugetSpecDependencies(doc));

            return list.ToArray();
        }

        public string[] GetFilePatternsFromNugetSpecFiles(XmlDocument doc)
        {
            var nodes = doc.SelectNodes("//files/file");

            var list = new List<string>();

            foreach (XmlNode node in nodes)
            {
                list.Add(node.Attributes["src"].Value);
            }

            return list.ToArray();
        }

        public string[] GetFilePatternsFromNugetSpecDependencies(XmlDocument doc)
        {
            var nodes = doc.SelectNodes("//dependency");

            var list = new List<string>();

            foreach (XmlNode node in nodes)
            {
                var packageId = node.Attributes["id"].Value;

                var version = node.Attributes["version"].Value;

                // Remove any of the nuget special characters used with versions
                version = version.Replace("[", "");
                version = version.Replace("]", "");
                version = version.Replace("(", "");
                version = version.Replace(")", "");

                var fullPattern = "lib/" + packageId + "." + version + "/**";

                list.Add(fullPattern);
            }

            return list.ToArray();
        }
    }
}

