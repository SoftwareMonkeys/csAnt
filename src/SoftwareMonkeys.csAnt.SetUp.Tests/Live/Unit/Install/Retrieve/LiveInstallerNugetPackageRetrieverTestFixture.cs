using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp.Install.Retrieve;
using SoftwareMonkeys.csAnt.External.Nuget.Tests;
using System.IO;

namespace SoftwareMonkeys.csAnt.SetUp.Tests.Live.Unit.Install.Retrieve
{
    [TestFixture]
    public class LiveInstallerNugetPackageRetrieverTestFixture : BaseLiveSetUpUnitTestFixture
    {
        public LiveInstallerNugetPackageRetrieverTestFixture ()
        {
        }

        [Test]
        public void Test_GetVersions()
        {
            new FileCopier(
                OriginalDirectory,
                WorkingDirectory
                ).Copy("lib/nuget.exe");

            var sourcePath = "https://www.myget.org/F/softwaremonkeys/"; // TODO: Make this path configurable

            var retriever = new InstallerNugetPackageRetriever();
            retriever.NugetSourcePath = sourcePath;

            var versions = retriever.GetVersions("csAnt");

            foreach (var version in versions)
            {
                Console.WriteLine(version);
            }

            Assert.IsTrue(versions.Length > 0, "No versions identified.");
        }
    }
}

