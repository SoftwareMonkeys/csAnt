using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.External.Nuget;
using System.IO;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.SetUp.Update;
using SoftwareMonkeys.csAnt.SetUp.Install;
using SoftwareMonkeys.csAnt.Tests.Helpers;


namespace SoftwareMonkeys.csAnt.SetUp.Tests.Unit
{
    [TestFixture]
    public class UpdaterTestFixture : BaseSetUpUnitTestFixture
    {
        [Test]
        public void Update()
        {
            Prepare("1.0.0.0");

            //ModifyFile();

            var updater = new Updater(
                CreateMockInstallerRetriever(
                    OriginalDirectory,
                    WorkingDirectory
                )
            );

            updater.Version = new Version("2.0.0.0");

            updater.Update();
            
            // Check that csAnt still runs
            new HelloWorldScriptLauncher().Launch();
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

            var installer = new Installer(
                CreateMockInstallerRetriever(
                    OriginalDirectory,
                    WorkingDirectory
                )
            );

            installer.Install();
        }
    }
}

