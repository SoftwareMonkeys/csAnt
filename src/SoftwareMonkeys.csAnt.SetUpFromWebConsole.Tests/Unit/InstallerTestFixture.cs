using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.External.Nuget;


namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.Tests.Unit
{
    [TestFixture]
    public class InstallerTestFixture : BaseSetUpUnitTestFixture
    {
        [Test]
        public void Install()
        {
            var installer = new Installer();

            // Assign mock nuget components to avoid actually using nuget during the test (fas
            installer.NugetChecker = CreateMockNugetChecker(false);
            installer.NugetExecutor = CreateMockNugetExecutor("0.0.0.1");

            installer.Install();

            var script = GetDummyScript();

            // TODO: Is launching a hello world script the best way to check that the installation worked considering this is a unit test and not an integration test?

            script.ExecuteScript("HelloWorld");

            Assert.IsFalse(script.IsError, "An error occurred.");
        }

        public NugetChecker CreateMockNugetChecker(bool checkForNuget)
        {
            return new MockNugetChecker()
            {
                CheckForNuget = checkForNuget
            };
        }

        public NugetExecutor CreateMockNugetExecutor(string version)
        {
            return new MockNugetExecutor(OriginalDirectory, WorkingDirectory, version);
        }
    }
}

