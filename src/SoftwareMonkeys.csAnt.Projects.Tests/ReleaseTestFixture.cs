using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
    [TestFixture]
    public class ReleaseTestFixture : BaseProjectsTestFixture
    {
        [Test]
        public void Test_Release()
        {
            var script = (BaseProjectScript)GetDummyScript();

            script.FilesGrabber.GrabOriginalScriptingFiles();

            var dummyFile = script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "testing"
                + Path.DirectorySeparatorChar
                    + "TestFile.txt";

            var dummyContent = "Hello world";

            script.EnsureDirectoryExists(Path.GetDirectoryName(dummyFile));

            File.WriteAllText(dummyFile, dummyContent);

            var releaseListFile = script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "rls"
                + Path.DirectorySeparatorChar
                    + "test-list.txt";

            var releaseListFileContent = "/testing/*.txt";

            script.EnsureDirectoryExists(Path.GetDirectoryName(releaseListFile));

            File.WriteAllText(releaseListFile, releaseListFileContent);

            script.Release("Test");

            var releaseFileName = script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "rls"
                + Path.DirectorySeparatorChar
                + "test"
                + Path.DirectorySeparatorChar
                + "csAnt-test-["
                + script.TimeStamp
                    + "].zip";

            Assert.IsTrue (File.Exists(releaseFileName));

            var unzipPath = script.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "unzip";

            script.EnsureDirectoryExists(unzipPath);

            script.Unzip(releaseFileName, unzipPath);

            Assert.AreEqual(1, Directory.GetFiles(unzipPath, "*", SearchOption.AllDirectories).Length, "Wrong number of files found.");
        }
    }
}

