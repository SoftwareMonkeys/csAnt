using System;
using System.IO;
using NUnit.Framework;
using SoftwareMonkeys.FileNodes;
using SoftwareMonkeys.csAnt.External.Nuget.Tests;
using SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp.Install.Retrieve;

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

            new FileNodeManager().EnsureNodes();

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
    }
}

