using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Versions;


namespace SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock
{
    public class MockNugetFeedCreator
    {
        public string FeedPath { get;set; }

        // TODO: Remove if not needed. Not currently in use.
        public string OriginalDirectory { get;set; }

        public string WorkingDirectory { get;set; }

        public bool IncludeProjectPackages = true;

        public MockNugetFeedCreator (string originalDirectory, string workingDirectory, string feedPath)
        {
            OriginalDirectory = originalDirectory;
            WorkingDirectory = workingDirectory;
            FeedPath = feedPath;
        }

        public MockNugetFeedCreator (string originalDirectory, string workingDirectory)
        {
            OriginalDirectory = originalDirectory;
            WorkingDirectory = workingDirectory;
            FeedPath = Path.Combine(workingDirectory, "../TestNugetFeed");
        }

        public void Create()
        {
            var nuget = new NugetPacker(WorkingDirectory);
            nuget.Version = new Version(new VersionManager().GetVersion(WorkingDirectory));

            var pkgDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "pkg";

            var specFile = pkgDir
                    + Path.DirectorySeparatorChar
                    + "csAnt.nuspec";

            // TODO: Perform packaging in temporary directory instead of working directory
            nuget.PackageFile(specFile);

            var pkgFile = FileNavigator.GetNewestFile(
                pkgDir
                + Path.DirectorySeparatorChar
                + "csAnt"
            );

            var pkgToFile = FeedPath
                + Path.DirectorySeparatorChar
                    + Path.GetFileName(pkgFile);

            DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(pkgToFile));

            File.Copy(pkgFile, pkgToFile, true);

            GrabRequiredPackages(WorkingDirectory, FeedPath);
        }

        public void GrabRequiredPackages(string workingDirectory, string feedPath)
        {
            Console.WriteLine("");
            Console.WriteLine("Getting required packages...");
            
            var pkgDirs = new string[] {
                "pkg",
                "lib"
            };

            // TODO: Reorganize this code so it's simpler to read. Use FilesGrabber utility.
            foreach (var pkgDir in pkgDirs) {
                var fullPkgDir = Path.Combine (workingDirectory, pkgDir);
                // Get the nuget.exe file
                var nugetFilePath = Path.Combine (fullPkgDir, "nuget.exe");
                if (File.Exists (nugetFilePath)) {
                    var toFile = nugetFilePath.Replace (fullPkgDir, feedPath);
                    File.Copy (nugetFilePath, toFile, true);
                }
                foreach (var dir in Directory.GetDirectories(fullPkgDir)) {
                    if (IncludeProjectPackages
                        || !Path.GetFileName (dir).StartsWith ("csAnt")) {
                        // Get the package files
                        foreach (var file in Directory.GetFiles(dir, "*.nupkg", SearchOption.AllDirectories)) {
                            var toFile = file.Replace (fullPkgDir, feedPath);
    
                            DirectoryChecker.EnsureDirectoryExists (Path.GetDirectoryName (toFile));

                            Console.WriteLine ("  " + toFile.Replace (feedPath, ""));

                            File.Copy (file, toFile, true);
                        }
                    }
                }
            }

        }
    }
}

