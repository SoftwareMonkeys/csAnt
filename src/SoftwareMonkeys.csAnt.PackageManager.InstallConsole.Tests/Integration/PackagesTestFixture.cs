using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.PackageManager.Schema;
using System.IO;

namespace SoftwareMonkeys.csAnt.PackageManager.Tests.Integration
{
    [TestFixture]
    public class PackageManagerTestFixture : BasePackageManagerTestFixture
    {
        [Test]
        public void Test_CreateLoadAddSave()
        {
            var repositoryManager = new RepositoryManager();

            var script = GetDummyScript();

            var repoPath = script.GetTmpDir();

            repositoryManager.Create ("Local", repoPath);

            var repository = repositoryManager.Load(repoPath);

            var package = new PackageInfo("TestPackage");

            repository.Packages.Add (package);

            repositoryManager.Save(repository);
            
            Console.WriteLine("Repository file output:");
            Console.WriteLine ("");
            Console.Write(File.ReadAllText(repository.FilePath));
            Console.WriteLine ("");
        }
    }
}

