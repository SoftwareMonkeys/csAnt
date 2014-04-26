using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Versions;


namespace SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock
{
    public class MockNugetFeedCreator
    {
        public string FeedPath { get;set; }

        public string OriginalDirectory { get;set; }

        public string WorkingDirectory { get;set; }

        public MockNugetFeedCreator (string originalDirectory, string workingDirectory, string feedPath)
        {
            OriginalDirectory = originalDirectory;
            WorkingDirectory = workingDirectory;
            FeedPath = feedPath;
        }

        public void Create()
        {
            var nuget = new NugetPacker(WorkingDirectory);
            nuget.Version = new Version(new VersionManager().GetVersion(OriginalDirectory));

            var pkgDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "pkg";

            var specFile = pkgDir
                    + Path.DirectorySeparatorChar
                    + "csAnt.nuspec";

            // TODO: Perform packaging in temporary directory instead of main project
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

            GrabRequiredPackages(OriginalDirectory, FeedPath);
        }

        public void GrabRequiredPackages(string originalDirectory, string feedPath)
        {
            Console.WriteLine("");
            Console.WriteLine("Getting required packages...");

            var pkgDir = Path.Combine(originalDirectory, "pkg");

            foreach (var dir in Directory.GetDirectories(pkgDir))
            {
                if (!Path.GetFileName(dir).StartsWith("csAnt"))
                {
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        var toFile = file.Replace(pkgDir, feedPath);
    
                        DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(toFile));

                        Console.WriteLine("  " + toFile.Replace(feedPath, ""));

                        File.Copy(file, toFile, true);
                    }
                }
            }

        }
    }
}

