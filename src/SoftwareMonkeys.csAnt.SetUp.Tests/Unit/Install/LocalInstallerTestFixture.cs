using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.SetUp;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp.Install;


namespace SoftwareMonkeys.csAnt.SetUp.Tests.Unit
{
    [TestFixture]
    public class LocalInstallerTestFixture : BaseSetUpUnitTestFixture
    {
        
        [Test]
        public void Test_Install()
        {
            var installer = new LocalInstaller(
                OriginalDirectory,
                WorkingDirectory,
                "csAnt",
                true
            );

            installer.Install();
        }

        [Test]
        public void Test_Install_Import()
        {
            var installer = new LocalInstaller(
                OriginalDirectory,
                WorkingDirectory,
                "csAnt",
                true
            );

            installer.ImportPath = OriginalDirectory;
            installer.Import = true;

            installer.Install();
        }
        
        [Test]
        public void Test_Install_Clone()
        {
            var installer = new LocalInstaller(
                OriginalDirectory,
                WorkingDirectory,
                "csAnt",
                true
            );

            installer.CloneSource = OriginalDirectory;
            installer.Clone = true;

            installer.Install();

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
    }
}

