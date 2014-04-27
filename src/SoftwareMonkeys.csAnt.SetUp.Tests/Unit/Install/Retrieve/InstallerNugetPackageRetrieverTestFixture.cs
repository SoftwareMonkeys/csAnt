using System;
using SoftwareMonkeys.csAnt.SetUp.Install.Retrieve;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.External.Nuget.Tests;

namespace SoftwareMonkeys.csAnt.SetUp.Tests.Unit
{
    public class InstallerNugetPackageRetrieverTestFixture : BaseSetUpUnitTestFixture
    {
        [Test]
        public void Test_Retrieve_SpecifyStatus()
        {
            // Copy the nuget.exe file into the current test directory so the retriever can skip downloading it
            new FileCopier(
                OriginalDirectory,
                CurrentDirectory
                ).Copy(
                    "lib/nuget.exe"
                );

            new NodeManager().EnsureNodes();

            new MockNugetPackageCreator().Create("TestPackage", new Version("1.0.0"), "beta");
            new MockNugetPackageCreator().Create("TestPackage", new Version("1.0.1"), "alpha");

            var retriever = new InstallerNugetPackageRetriever()
            {
                NugetSourcePath = PathConverter.ToAbsolute("pkg"),
                NugetPath = Path.Combine(OriginalDirectory, "lib/nuget.exe") // This shouldn't be required but leave it in just to ensure the test never tries to download the file from the web
            };

            retriever.Retrieve("TestPackage", new Version(0,0,0,0), "beta");

            var expectedDir = PathConverter.ToAbsolute("lib/TestPackage.1.0.0-beta");

            Assert.IsTrue(Directory.Exists(expectedDir), "The expected lib directory wasn't found.");
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

            var retriever = new InstallerNugetPackageRetriever();
            retriever.NugetSourcePath = sourcePath;

            var version = retriever.GetVersion("TestPackage", new Version("1.0"), "beta");

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

            var retriever = new InstallerNugetPackageRetriever();
            retriever.NugetSourcePath = sourcePath;

            var versions = retriever.GetVersions("TestPackage");

            Console.WriteLine(versions[0]);

            Assert.AreEqual("1.0.0-beta", versions[0], "Version mismatch.");
        }

        [Test]
        public void Test_VersionMatches()
        {
            var retriever = new InstallerNugetPackageRetriever();

            bool matches = retriever.VersionMatches("1.0.0-beta", new Version("1.0"), "beta");

            Assert.IsTrue(matches, "Didn't match");
        }

        [Test]
        public void Test_VersionMatches_NoStatus()
        {
            var retriever = new InstallerNugetPackageRetriever();

            bool matches = retriever.VersionMatches("1.0.0", new Version("1.0"), "");

            Assert.IsTrue(matches, "Didn't match");
        }
        
        [Test]
        public void Test_VersionMatches_MismatchVersion()
        {
            var retriever = new InstallerNugetPackageRetriever();

            bool matches = retriever.VersionMatches("1.0.0-beta", new Version("1.1"), "beta");

            Assert.IsFalse(matches, "Matched when it shouldn't.");
        }

        [Test]
        public void Test_VersionMatches_MismatchStatus()
        {
            var retriever = new InstallerNugetPackageRetriever();

            bool matches = retriever.VersionMatches("1.0.0-beta", new Version("1.0"), "alpha");
            
            Assert.IsFalse(matches, "Matched when it shouldn't.");
        }

        public void Prepare()
        {
            // TODO: Figure out a better way to prepare for the test without a whole package cycle
            /*new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
                ).GrabOriginalFiles();

            new ScriptLauncher().Launch("CyclePackage");*/
        }
    }
}

