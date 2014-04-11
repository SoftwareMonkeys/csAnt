using System;
using NUnit.Framework;
using System.Net;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Versions;


namespace SoftwareMonkeys.csAnt.External.Nuget.Tests.Unit
{
    [TestFixture]
    [Category("Unit")]
    public class NugetPackerUnitTestFixture : BaseNugetUnitTestFixture
    {
        [Test]
        public void Test_Pack()
        {
            // TODO: Turn this test into a unit test

            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
                ).GrabOriginalFiles();

            var packageName = "csAnt";

            var version = "0.0.0.1";

            // TODO: Remove dependency on existing .nuspec file by creating a mock one
            var filePath = WorkingDirectory
                + Path.DirectorySeparatorChar
                + "pkg"
                + Path.DirectorySeparatorChar
                + packageName + ".nuspec";

            var packer = new NugetPacker();
            packer.Version = new Version(version);
            packer.PackageFile(filePath);

            var pkgFilePath = Path.GetDirectoryName(filePath)
                + Path.DirectorySeparatorChar
                    + packageName
                    + Path.DirectorySeparatorChar
                    + packageName + "." + version
                    + ".nupkg";

            Console.WriteLine("Expected package file:");
            Console.WriteLine(pkgFilePath);

            Assert.IsTrue(File.Exists(pkgFilePath), "Package file not found.");
        }
    }
}

