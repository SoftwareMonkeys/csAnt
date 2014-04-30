using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.SetUp.Install.Unpack;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.SetUp.Tests.Unit
{
    public class InstallUnpackerTestFixture : BaseSetUpUnitTestFixture
    {
        [Test]
        public void Test_GetCsAntPackageDir()
        {
            var packageDir = Path.Combine(CurrentDirectory, "lib/csAnt.1.2.3.4");
            var generalDir = Path.Combine(CurrentDirectory, "lib/csAnt");

            DirectoryChecker.EnsureDirectoryExists(packageDir);
            DirectoryChecker.EnsureDirectoryExists(generalDir);

            var unpacker = new InstallUnpacker();
            var dir = unpacker.GetcsAntPackageDir("lib", new Version(0,0,0,0));

            Assert.AreEqual(packageDir, dir, "Wrong directory.");
        }
    }
}

