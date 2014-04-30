using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.External.Nuget.Tests;
using System.IO;
using SoftwareMonkeys.csAnt.External.Nuget;

namespace SoftwareMonkeys.csAnt.External.Nuget.Tests.Live.Unit
{
    [TestFixture]
    public class LiveVersionerTestFixture : BaseLiveNugetUnitTestFixture
    {
        public LiveVersionerTestFixture ()
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

            var versioner = new NugetVersioner(sourcePath);

            var versions = versioner.GetVersions("csAnt");

            foreach (var version in versions)
            {
                Console.WriteLine(version);
            }

            Assert.IsTrue(versions.Length > 0, "No versions identified.");
        }
    }
}

