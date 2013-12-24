using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages.Tests.Integration
{
    [TestFixture]
    public class PackageRetrieverTestFixture : BasePackagesTestFixture
    {
        [Test]
        public void Test_Get()
        {
            var script = GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory, 
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var packageName = "TestPackage";

            var groupName = "TestCompany";

            var packages = new PackageManager(
                WorkingDirectory
            );

            // Create a test package
            packages.Create(packageName, groupName);

            // Add a bunch of files to the package (.pkg file)
            packages.AddFiles (packageName, groupName, "script/*.cs");

            // Build the package into a zip file
            packages.Build(packageName, groupName, "1.0.0.0");

            // Create a path for a repository
            var repoPath = script.GetTmpDir()
                + Path.DirectorySeparatorChar
                    + "pkgs";

            // Send the package to the repository
            packages.Send(packageName, groupName, repoPath);

            packages.WorkingDirectory = WorkingDirectory
                + Path.DirectorySeparatorChar
                + ".."
                + Path.DirectorySeparatorChar
                + "NewProject";

            if (!Directory.Exists(packages.WorkingDirectory))
                Directory.CreateDirectory(packages.WorkingDirectory);

            // Pull the package from the repository to the current working directory
            packages.Pull(packageName, groupName, repoPath);
        }
    }
}

