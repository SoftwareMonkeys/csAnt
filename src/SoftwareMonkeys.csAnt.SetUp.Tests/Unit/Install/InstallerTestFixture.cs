using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.External.Nuget;
using SoftwareMonkeys.csAnt.SetUp;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp.Install;
using SoftwareMonkeys.csAnt.Tests.Helpers;


namespace SoftwareMonkeys.csAnt.SetUp.Tests.Unit
{
    [TestFixture]
    public class InstallerTestFixture : BaseSetUpUnitTestFixture
    {
        [Test]
        public void Test_Install()
        {
            MoveToTestProjectDir ();

            var installer = new Installer(
                CreateMockRetriever(OriginalDirectory, WorkingDirectory)
            );

            installer.Install();
        }

        [Test]
        public void Test_Install_Import()
        {
            MoveToTestProjectDir ();

            var installer = new Installer(
                CreateMockInstallerRetriever(
                    OriginalDirectory,
                    WorkingDirectory
                )
            );

            installer.ImportPath = OriginalDirectory;
            installer.Import = true;

            installer.Install();
        }
        
        [Test]
        public void Test_Install_Clone()
        {
            MoveToTestProjectDir ();

            var installer = new Installer(
                CreateMockInstallerRetriever(
                    OriginalDirectory,
                    WorkingDirectory
                )
            );

            installer.CloneSource = OriginalDirectory;
            installer.Clone = true;

            installer.Install();

            // Check that csAnt still runs
            new HelloWorldScriptLauncher().Launch();

            Assert.IsTrue(
                Directory.Exists(PathConverter.ToAbsolute(".git")),
                "The .git folder wasn't found."
                );
        }
        
        public MockInstallerRetriever CreateMockRetriever(string source, string destination)
        {
            var retriever = new MockInstallerRetriever(source, destination);

            return retriever;
        }

        public void MoveToTestProjectDir()
        {
            var testProjectDir = Path.GetFullPath ("../TestProject");

            DirectoryChecker.EnsureDirectoryExists (testProjectDir);

            Environment.CurrentDirectory = testProjectDir;

            WorkingDirectory = testProjectDir;
        }
    }
}

