using System;
using SoftwareMonkeys.csAnt.SetUp.Install.Retrieve;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.SetUp.Tests.Unit
{
    public class InstallerNugetPackageRetrieverTestFixture : BaseSetUpUnitTestFixture
    {
        [Test]
        public void Test_GetVersion()
        {
            // TODO: Should this be an integration test?

            Prepare();

            var sourcePath = Path.Combine(CurrentDirectory, "pkg");

            var retriever = new InstallerNugetPackageRetriever();
            retriever.NugetSourcePath = sourcePath;

            var version = retriever.GetVersion("csAnt", new Version("0.5"), "alpha");

            Console.WriteLine(version);
        }

        [Test]
        public void Test_VersionMatches()
        {
            var retriever = new InstallerNugetPackageRetriever();

            bool matches = retriever.VersionMatches("1.0.0-beta", new Version("1.0"), "beta");

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
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
                ).GrabOriginalFiles();

            new ScriptLauncher().Launch("CyclePackage");
        }
    }
}

