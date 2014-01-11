using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages.Tests.Integration
{
    [TestFixture]
    public class PackagesTestFixture : BasePackagesTestFixture
    {
        [Test]
        public void Test_PackagesCycle_CreateAddBuildSendPullInstall()
        {
            var script = GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory, 
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var packageName = "TestPackage";

            var groupName = "TestCompany";
            
            // Create a path for the temporary repository
            var repositoryPath = Path.GetFullPath(
                WorkingDirectory
                + Path.DirectorySeparatorChar
                + ".."
                + Path.DirectorySeparatorChar
                + "pkgs"
            );

            var packages = new PackageManager(
                WorkingDirectory
            );

            // Create a test package
            packages.Create(packageName, groupName);

            var testDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "TestDir";

            var testFile = testDir
                + Path.DirectorySeparatorChar
                    + "TestFile.txt";

            Directory.CreateDirectory(testDir);

            File.WriteAllText(testFile, "Hello world");

            // Add a bunch of files to the package (.pkg file)
            packages.AddFiles (packageName, groupName, "TestDir/*");

            // Build the package into a zip file
            packages.Build(packageName, groupName, "1.0.0.0");


            // Send the package to the repository
            packages.Send(packageName, groupName, repositoryPath);

            packages.WorkingDirectory = WorkingDirectory
                + Path.DirectorySeparatorChar
                + ".."
                + Path.DirectorySeparatorChar
                + "NewProject";

            if (!Directory.Exists(packages.WorkingDirectory))
                Directory.CreateDirectory(packages.WorkingDirectory);

            // Pull the package from the repository to the current working directory
            packages.Install(packageName, groupName, repositoryPath);

            var expectedFile = packages.WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "pkgs"
                + Path.DirectorySeparatorChar
                    + groupName
                + Path.DirectorySeparatorChar
                    + packageName
                + Path.DirectorySeparatorChar
                    + packageName + "-1-0-0-0"
                + Path.DirectorySeparatorChar
                    + "TestDir"
                + Path.DirectorySeparatorChar
                    + "TestFile.txt";

            Console.WriteLine ("Expected file:");
            Console.WriteLine (expectedFile);

            Assert.IsTrue(File.Exists(expectedFile), "Test file wasn't found in destination package directory.");
        }
    }
}

