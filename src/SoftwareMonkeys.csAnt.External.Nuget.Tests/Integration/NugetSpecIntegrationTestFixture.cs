using System;
using NUnit.Framework;
using System.Net;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Versions;


namespace SoftwareMonkeys.csAnt.External.Nuget.Tests
{
    [TestFixture]
    public class NugetSpecIntegrationTestFixture : BaseNugetIntegrationTestFixture
    {
        [Test]
        public void Test_Pack()
        {
            // TODO: Turn this test into a unit test

            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
                ).GrabOriginalFiles();

            var projectName = "csAnt";

            var releaseName = "standard-release";

            // TODO: Remove dependency on version stored in .node file. Mock the version and updage NugetPacker to allow it
            var version = new VersionManager().GetVersion(WorkingDirectory);

            // TODO: Remove dependency on existing .nuspec file by creating a mock one
            var filePath = WorkingDirectory
                + Path.DirectorySeparatorChar
                + "pkg"
                + Path.DirectorySeparatorChar
                + projectName + "-" + releaseName + ".nuspec";

            var packer = new NugetPacker();
            packer.PackFile(filePath);

            var pkgFilePath = Path.GetDirectoryName(filePath)
                + Path.DirectorySeparatorChar
                    + projectName + "-" + releaseName
                    + Path.DirectorySeparatorChar
                    + projectName + "-" + releaseName + "." + version
                    + ".nupkg";

            Console.WriteLine("Expected package file:");
            Console.WriteLine(pkgFilePath);

            Assert.IsTrue(File.Exists(pkgFilePath), "Package file not found.");
        }
    }
}

