using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;
using System.IO;

namespace SoftwareMonkeys.csAnt.External.Nuget.Tests
{
    public class NugetVersionerTestFixture : BaseNugetUnitTestFixture
    {
        public NugetVersionerTestFixture ()
        {
        }

        [Test]
        public void Test_GetVersion()
        {
            new FileCopier(
                OriginalDirectory,
                WorkingDirectory
            ).Copy("lib/nuget.exe");

            new MockNugetPackageCreator().Create("TestPackage", new Version("1.0.0"), "beta");

            var sourcePath = Path.Combine(CurrentDirectory, "pkg");

            var versioner = new NugetVersioner(sourcePath);

            var version = versioner.GetVersion("TestPackage", new Version("1.0"), "beta");

            Console.WriteLine(version);

            Assert.AreEqual("1.0.0", version.ToString(), "Version mismatch.");
        }

        [Test]
        public void Test_GetVersions()
        {
            new FileCopier(
                OriginalDirectory,
                WorkingDirectory
            ).Copy("lib/nuget.exe");

            new MockNugetPackageCreator().Create("TestPackage", new Version("1.0.0"), "beta");

            var sourcePath = Path.Combine(CurrentDirectory, "pkg");

            var versioner = new NugetVersioner(sourcePath);

            var versions = versioner.GetVersions("TestPackage");

            Console.WriteLine(versions[0]);

            Assert.AreEqual("1.0.0-beta", versions[0], "Version mismatch.");
        }

        [Test]
        public void Test_VersionMatches()
        {
            var versioner = new NugetVersioner();

            bool matches = versioner.VersionMatches("1.0.0-beta", new Version("1.0"), "beta");

            Assert.IsTrue(matches, "Didn't match");
        }

        [Test]
        public void Test_VersionMatches_NoStatus()
        {
            var versioner = new NugetVersioner();

            bool matches = versioner.VersionMatches("1.0.0", new Version("1.0"), "");

            Assert.IsTrue(matches, "Didn't match");
        }

        [Test]
        public void Test_VersionMatches_MismatchVersion()
        {
            var versioner = new NugetVersioner();

            bool matches = versioner.VersionMatches("1.0.0-beta", new Version("1.1"), "beta");

            Assert.IsFalse(matches, "Matched when it shouldn't.");
        }

        [Test]
        public void Test_VersionMatches_MismatchStatus()
        {
            var versioner = new NugetVersioner();

            bool matches = versioner.VersionMatches("1.0.0-beta", new Version("1.0"), "alpha");

            Assert.IsFalse(matches, "Matched when it shouldn't.");
        }

    }
}

