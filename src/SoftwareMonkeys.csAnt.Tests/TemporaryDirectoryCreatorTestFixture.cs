using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class TemporaryDirectoryCreatorTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_GetTmpDir_Standard()
        {
            var timeStamp = GetTimeStamp();

            var tdc = new TemporaryDirectoryCreator(
                WorkingDirectory,
                timeStamp,
                true
            );

            // TODO: Remove if not needed
            //script.CurrentDirectory = script.OriginalDirectory;

            var tmpDir = tdc.GetTmpDir();

			Console.WriteLine ("");
			Console.WriteLine ("Tmp dir:");
			Console.WriteLine (tmpDir);
			Console.WriteLine ("");

            var expectedDir = Path.GetDirectoryName(WorkingDirectory)
                + Path.DirectorySeparatorChar
                + "csAnt.tmp"
                + Path.DirectorySeparatorChar
                + timeStamp
                + Path.DirectorySeparatorChar
                + "csAnt";

            Assert.AreEqual(expectedDir, tmpDir, "Paths don't match.");
        }
        
        [Test]
        public void Test_GetTmpDir_WithinTmp()
        {
            var script = GetDummyScript ();

            var tmpDir = script.GetTmpDir();

            Console.WriteLine ("");
            Console.WriteLine ("Tmp dir:");
            Console.WriteLine (tmpDir);
            Console.WriteLine ("");
            
            Console.WriteLine ("");
            Console.WriteLine ("Original dir:");
            Console.WriteLine (script.OriginalDirectory);
            Console.WriteLine ("");

            var expectedDir = Path.GetDirectoryName(script.OriginalDirectory).TrimEnd(Path.DirectorySeparatorChar)
                + Path.DirectorySeparatorChar
                + "csAnt.tmp"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "csAnt.tmp"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "csAnt";

            Assert.AreEqual(expectedDir, tmpDir, "Paths don't match.");
        }
        
        
        [Test]
        public void Test_GetTmpDir_DeepWithinTmp()
        {
            var script = GetDummyScript ();

            var tmpDir = script.GetTmpDir();
            tmpDir = script.GetTmpDir();

            Console.WriteLine ("");
            Console.WriteLine ("Tmp dir:");
            Console.WriteLine (tmpDir);
            Console.WriteLine ("");

            var expectedDir = Path.GetDirectoryName(script.OriginalDirectory)
                + Path.DirectorySeparatorChar
                + "csAnt.tmp"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "csAnt.tmp"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "csAnt";

            Assert.AreEqual(expectedDir, tmpDir, "Paths don't match.");
        }
    }
}

