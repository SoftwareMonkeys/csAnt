using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.Packages;
using SoftwareMonkeys.csAnt.Packages.Schema;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages.Tests
{
    [TestFixture]
    public class PackageSaverTestFixture : BasePackagesTestFixture
    {
        [Test]
        public void Test_Save_NoFilePath()
        {
            var script = GetDummyScript();

            var dir = script.CurrentDirectory;

            var saver = new PackageSaver();

            var pkg = new PackageInfo("TestPackage", "TestCompany");

            var filePath = saver.Save (dir, pkg);

            Assert.IsTrue (File.Exists(filePath), "File not saved.");
        }
    }
}

