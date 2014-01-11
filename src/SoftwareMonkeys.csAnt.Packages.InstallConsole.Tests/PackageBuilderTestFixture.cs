using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO.Compression;
using SoftwareMonkeys.csAnt.IO;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages.Tests
{
    [TestFixture]
    public class PackageBuilderTestFixture : BasePackagesTestFixture
    {
        [Test]
        public void Test_Build()
        {
            var packageName = "TestPackage";

            var groupName = "TestGroup";

            var script = GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            // TODO: Use a mock creator
            var creator = new PackageCreator();

            // TODO: Use a mock loader
            var loader = new PackageLoader();

            // TODO: Use a mock file finder
            var fileFinder = new FileFinder();

            var builder = new PackageBuilder(
                loader,
                new FileZipper(
                    fileFinder,
                    new DirectoryMover()
                ),
                new PackageZipFileNamer()
            );

            // TODO: Use a mock saver
            var saver = new PackageSaver();

            // TODO: Use a mock adder
            var adder = new PackageFileAdder(
                fileFinder,
                loader,
                saver
            );

            var pkg = creator.Create(packageName, groupName);

            var packagesDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "pkgs";

            saver.Save(packagesDir, pkg);

            adder.AddTo (packagesDir, packageName, groupName, "scripts/*.cs");

            var zipFilePath = builder.Build(WorkingDirectory, packagesDir, packageName, groupName, "1.0.0.0");

            Assert.IsTrue(File.Exists(zipFilePath), "Zip file not found.");
        }

    }
}

