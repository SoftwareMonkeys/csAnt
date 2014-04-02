using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.IO;
using SoftwareMonkeys.csAnt.SetUp.Common;


namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.Tests.Unit
{
    [TestFixture]
    public class UpdaterTestFixture : BaseSetUpUnitTestFixture
    {
        [Test]
        public void Update()
        {
            Prepare("1.0.0.0");

            //ModifyFile();

            var updater = new Updater();

            // Assign mock nuget components to avoid actually using nuget during the test (fas
            updater.Installer.NugetChecker = CreateMockNugetChecker(false);
            updater.Installer.NugetExecutor = CreateMockNugetExecutor("2.0.0.0");

            updater.Update("csAnt", true);

            var script = GetDummyScript();

            // TODO: Is launching a hello world script the best way to check that the installation worked considering this is a unit test and not an integration test?

            script.ExecuteScript("HelloWorld");

            Assert.IsFalse(script.IsError, "An error occurred.");

            //CheckModifiedFile();
        }

        public void Prepare(string version)
        {
            MockInstall(version);

            var script = GetDummyScript();
            script.CreateNodes();
        }

        public void CheckModifiedFile()
        {
            var file = GetHelloWorldFile();

            var content = File.ReadAllText(file);

            // Check that the content no longer contains "ModifiedText" because it should have been updated
            Assert.IsFalse(
                content.Contains("ModifiedText"),
                "The file content must not have been updated."
                );
        }

        public void ModifyFile()
        {
            var file = GetHelloWorldFile();

            var content = File.ReadAllText(file);

            content.Replace("Hello world", "ModifiedText");

            File.WriteAllText(file, content);
        }

        public string GetHelloWorldFile()
        {
            return WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "scripts"
                    + Path.DirectorySeparatorChar
                    + "HelloWorld.cs";
        }

        public void MockInstall(string version)
        {
            // TODO: Move the files directly without using the installer, because this is a unit test and should only be relying on one component

            var installer = new Installer();

            // Assign mock nuget components to avoid actually using nuget during the test (fas
            installer.NugetChecker = CreateMockNugetChecker(false);
            installer.NugetExecutor = CreateMockNugetExecutor(version);

            installer.Install("csAnt");
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

