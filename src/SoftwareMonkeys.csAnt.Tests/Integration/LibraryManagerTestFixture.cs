using System;
using System.IO;
using NUnit.Framework;
using SoftwareMonkeys.FileNodes;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.Tests.Integration
{
    [TestFixture]
    public class LibraryManagerTestFixture : BaseIntegrationTestFixture
    {
        public LibraryManagerTestFixture ()
        {
        }

        [Test]
        public void Test_Nuget()
        {
            var script = GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                WorkingDirectory
            ).GrabOriginalScriptingFiles();

            var nodeManager = new FileNodeManager();

            nodeManager.CreateNodes();

            var libraryManager = new LibraryManager(nodeManager.State);

            libraryManager.AddNuget("TestLib", "NugetDummy1");

            libraryManager.Get("TestLib");

            var expectedDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                + "lib"
                    + Path.DirectorySeparatorChar
                    + "NugetDummy1.0.9.4342.40575";

            Assert.IsTrue(Directory.Exists(expectedDir), "The dummy library wasn't retrieved.");
        }
    }
}

