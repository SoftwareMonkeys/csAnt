using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Unit
{
    public class PackageCheckerUnitTestFixture : BaseProjectsUnitTestFixture
    {
        public PackageCheckerUnitTestFixture ()
        {
        }

        [Test]
        public void Test_GetVersionFromPackageFile()
        {
            var currentVersion = new Version(2,0,0,0);

            var checker = new PackageChecker(currentVersion);

            var version = "1.2.3.4";

            new FilesGrabber (OriginalDirectory, WorkingDirectory).GrabOriginalFiles ();

            var nodeManager = new FileNodeManager (WorkingDirectory);
            nodeManager.CurrentNode.Properties ["Version"] = version;
            nodeManager.CurrentNode.Save ();

            // TODO: Move this to a helper utility
            // Clear existing package files
            foreach (var dir in Directory.GetDirectories(Path.Combine (WorkingDirectory, "pkg")))
            {
                Directory.Delete (dir, true);
            }

            var fileName = new NugetPacker (WorkingDirectory).Pack (WorkingDirectory, "csAnt"); // TODO: Should the package name be hard coded here?

            //var fileName = PathConverter.ToAbsolute("pkg/csAnt/csAnt." + version + ".nupkg");

            var returnedVersion = checker.GetVersionFromPackageFile(fileName);

            Assert.AreEqual(version, returnedVersion.ToString(), "Wrong version.");
        }
        
        /*[Test]
        public void Test_GetVersionFromPackageFile_BranchAndStatus()
        {
            var currentVersion = new Version(2,0,0,0);

            var checker = new PackageChecker(currentVersion);

            var version = "1.2.3.4";

            var fileName = PathConverter.ToAbsolute("pkg/csAnt/csAnt." + version + "-alpha-branch.nupkg");

            var returnedVersion = checker.GetVersionFromPackageFile(fileName);

            Assert.AreEqual(version, returnedVersion.ToString(), "Wrong version.");
        }*/
    }
}

