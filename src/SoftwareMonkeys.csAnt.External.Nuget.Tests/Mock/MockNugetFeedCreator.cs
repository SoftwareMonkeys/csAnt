using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock
{
    public class MockNugetFeedCreator
    {
        public string FeedPath { get;set; }

        public string WorkingDirectory { get;set; }

        public MockNugetFeedCreator (string workingDirectory, string feedPath)
        {
            WorkingDirectory = workingDirectory;
            FeedPath = feedPath;
        }

        public void Create()
        {
            var nuget = new NugetPacker(WorkingDirectory);
            nuget.Version = new Version(0,1,0,0);

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
        }
    }
}

