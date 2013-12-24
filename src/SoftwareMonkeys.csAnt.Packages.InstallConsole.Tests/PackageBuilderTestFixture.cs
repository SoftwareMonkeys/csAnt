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

            var script = GetDummyScript();

            script.FilesGrabber.GrabOriginalScriptingFiles();

            var creator = new PackageCreator();

            var loader = new LocalPackageLoader();

            var fileFinder = new FileFinder();

            var builder = new PackageBuilder(
                loader,
                new FileZipper(
                    fileFinder
                )
            );

            var saver = new PackageSaver();

            var adder = new PackageFileAdder(
                fileFinder,
                loader,
                saver
            );

            var pkg = creator.Create(packageName);

            saver.Save(WorkingDirectory, pkg);

            adder.AddTo (WorkingDirectory, packageName, "scripts/*.cs");

            var zipFilePath = builder.Build(WorkingDirectory, packageName);

            Assert.IsTrue(File.Exists(zipFilePath), "Zip file not found.");
        }

    }
}

