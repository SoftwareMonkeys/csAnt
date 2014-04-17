using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.External.Nuget;
using SoftwareMonkeys.csAnt.SetUp;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp.Install;


namespace SoftwareMonkeys.csAnt.SetUp.Tests.Unit
{
    [TestFixture]
    public class InstallerTestFixture : BaseSetUpUnitTestFixture
    {
        [Test]
        public void Test_Install()
        {
            var installer = new Installer(
                CreateMockRetriever(OriginalDirectory, WorkingDirectory)
            );

            installer.Install();

            var script = GetDummyScript();

            // TODO: Is launching a hello world script the best way to check that the installation worked considering this is a unit test and not an integration test?

            script.ExecuteScript("HelloWorld");

            Assert.IsFalse(script.IsError, "An error occurred.");
        }

        [Test]
        public void Test_Install_Import()
        {
            var installer = new Installer(
                CreateMockInstallerRetriever(
                    OriginalDirectory,
                    WorkingDirectory,
                    new Version("0.0.0.1")
                )
            );

            installer.ImportPath = OriginalDirectory;
            installer.Import = true;

            installer.Install();

            var script = GetDummyScript();

            // TODO: Is launching a hello world script the best way to check that the installation worked considering this is a unit test and not an integration test?

            script.ExecuteScript("HelloWorld");

            Assert.IsFalse(script.IsError, "An error occurred.");
        }
        
        [Test]
        public void Test_Install_Clone()
        {
            var installer = new Installer(
                CreateMockInstallerRetriever(
                    OriginalDirectory,
                    WorkingDirectory,
                    new Version("0.0.0.1")
                )
            );

            installer.CloneSource = OriginalDirectory;
            installer.Clone = true;

            installer.Install();

            var script = GetDummyScript();

            // TODO: Is launching a hello world script the best way to check that the installation worked considering this is a unit test and not an integration test?

            script.ExecuteScript("HelloWorld");

            Assert.IsFalse(script.IsError, "An error occurred.");

            Assert.IsTrue(
                Directory.Exists(PathConverter.ToAbsolute(".git")),
                "The .git folder wasn't found."
                );
        }

        // TODO: Remove if not needed
        /*public NugetChecker CreateMockNugetChecker(bool checkForNuget)
        {
            return new MockNugetChecker()
            {
                CheckForNuget = checkForNuget
            };
        }

        public NugetExecutor CreateMockNugetExecutor(Version version)
        {
            return new MockNugetExecutor(OriginalDirectory, WorkingDirectory, version);
        }*/
        
        public MockInstallerRetriever CreateMockRetriever(string source, string destination)
        {
            return CreateMockRetriever(source, destination, new Version("0.0.0.0"));
        }

        public MockInstallerRetriever CreateMockRetriever(string source, string destination, Version version)
        {
            var retriever = new MockInstallerRetriever(source, destination, version);

            return retriever;
        }
    }
}

