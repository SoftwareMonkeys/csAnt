using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class InstallTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_Install()
        {
            var script = GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            throw new NotImplementedException();
            /*script.Install("csAnt", true);

            Assert.IsFalse(script.IsError, "An error occurred.");
            
            var libDir = script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "lib";

            Assert.IsTrue(Directory.Exists(libDir), "lib dir not found.");

            var scriptsDir = script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "scripts";

            Assert.IsTrue(Directory.Exists(scriptsDir), "scripts dir not found.");*/
        }
    }
}

