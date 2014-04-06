using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;
using System.IO;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.External.Nuget;


namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.Tests
{
    [TestFixture]
    public class SetUpConsoleTestFixture : BaseSetUpConsoleUnitTestFixture
    {
        [Test]
        public void Test_SetUp()
        {
            var setupFileName = "csAnt-SetUp.exe";

            // TODO: Make it possible to use the debug version if currently in debug mode
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
            ).GrabOriginalFiles(
                setupFileName
            );

            var sourceNugetPath = OriginalDirectory
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "nuget.exe";

            var feedPath = CreateMockFeed();

            var processStarter = new DotNetProcessStarter();
            processStarter.Start(
                setupFileName,
                "-nuget=" + sourceNugetPath,
                "-feed=" + feedPath
            );

            Assert.IsFalse(processStarter.IsError);
        }
        
        [Test]
        public void Test_SetUp_Import()
        {
            var setupFileName = "csAnt-SetUp.exe";

            // TODO: Make it possible to use the debug version if currently in debug mode
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
            ).GrabOriginalFiles(
                setupFileName
            );

            var sourceNugetPath = OriginalDirectory
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "nuget.exe";

            var feedPath = CreateMockFeed();

            var importPath = OriginalDirectory;

            var processStarter = new DotNetProcessStarter();
            processStarter.Start(
                setupFileName,
                "-nuget=" + sourceNugetPath,
                "-feed=" + feedPath,
                "-import=" + importPath
            );

            Assert.IsFalse(processStarter.IsError);
        }

        public string CreateMockFeed()
        {
            var feedPath = Path.GetDirectoryName(WorkingDirectory)
                + Path.DirectorySeparatorChar
                    + "feed";

            var packer = new NugetPacker(OriginalDirectory);

            var pkgDir = OriginalDirectory
                + Path.DirectorySeparatorChar
                    + "pkg";

            var specFile = pkgDir
                    + Path.DirectorySeparatorChar
                    + "csAnt.nuspec";

            // TODO: Perform packaging in temporary directory instead of main project
            packer.PackFile(specFile);

            var pkgFile = FileNavigator.GetNewestFile(
                pkgDir
                + Path.DirectorySeparatorChar
                + "csAnt"
            );

            var pkgToFile = feedPath
                + Path.DirectorySeparatorChar
                    + Path.GetFileName(pkgFile);

            DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(pkgToFile));

            File.Copy(pkgFile, pkgToFile);

            return feedPath;
        }
    }
}

