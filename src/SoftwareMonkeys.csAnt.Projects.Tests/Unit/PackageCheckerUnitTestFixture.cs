using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Unit
{
    public class PackageCheckerUnitTestFixture : BaseProjectsUnitTestFixture
    {
        public PackageCheckerUnitTestFixture ()
        {
        }

        [Test]
        public void Test_GetVersionFromPackageFileName()
        {
            var currentVersion = new Version(2,0,0,0);

            var checker = new PackageChecker(currentVersion);

            var version = "1.2.3.4";

            var fileName = PathConverter.ToAbsolute("pkg/csAnt/csAnt." + version + ".nupkg");

            var returnedVersion = checker.GetVersionFromPackageFileName(fileName);

            Assert.AreEqual(version, returnedVersion.ToString(), "Wrong version.");
        }
        
        [Test]
        public void Test_GetVersionFromPackageFileName_BranchAndStatus()
        {
            var currentVersion = new Version(2,0,0,0);

            var checker = new PackageChecker(currentVersion);

            var version = "1.2.3.4";

            var fileName = PathConverter.ToAbsolute("pkg/csAnt/csAnt." + version + "-alpha-branch.nupkg");

            var returnedVersion = checker.GetVersionFromPackageFileName(fileName);

            Assert.AreEqual(version, returnedVersion.ToString(), "Wrong version.");
        }
    }
}

