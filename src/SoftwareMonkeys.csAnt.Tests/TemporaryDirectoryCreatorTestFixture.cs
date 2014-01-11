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
            var dummyScript = GetDummyScript();

            dummyScript.Relocate(dummyScript.OriginalDirectory);

            var timeStamp = TimeStamp;

            var tdc = new TemporaryDirectoryCreator(
                dummyScript.CurrentDirectory,
                timeStamp,
                true
            );

            var tmpDir = tdc.GetTmpDir();

			Console.WriteLine ("");
			Console.WriteLine ("Tmp dir:");
			Console.WriteLine (tmpDir);
			Console.WriteLine ("");

            var expectedDir = Path.GetDirectoryName(dummyScript.CurrentDirectory)
                + Path.DirectorySeparatorChar
                + "csAnt.tmp"
                + Path.DirectorySeparatorChar
                + timeStamp
                + Path.DirectorySeparatorChar
                + "csAnt";

            Console.WriteLine ("");
            Console.WriteLine ("Expected dir:");
            Console.WriteLine (expectedDir);
            Console.WriteLine ("Actual dir:");
            Console.WriteLine (tmpDir);
            Console.WriteLine ("");

            Assert.AreEqual(expectedDir, tmpDir, "Paths don't match.");
        }
        
        [Test]
        public void Test_GetTmpDir_WithinTmp()
        {
            var timeStamp = TimeStamp;

            var tdc = new TemporaryDirectoryCreator(
                WorkingDirectory,
                timeStamp,
                true
            );

            var tmpDir = tdc.GetTmpDir();

            Console.WriteLine ("");
            Console.WriteLine ("Tmp dir:");
            Console.WriteLine (tmpDir);
            Console.WriteLine ("");

            var expectedDir = Path.GetDirectoryName(WorkingDirectory).TrimEnd(Path.DirectorySeparatorChar)
                + Path.DirectorySeparatorChar
                + "csAnt.tmp"
                + Path.DirectorySeparatorChar
                + TimeStamp
                + Path.DirectorySeparatorChar
                + "csAnt";
            
            Console.WriteLine ("");
            Console.WriteLine ("Expected dir:");
            Console.WriteLine (expectedDir);
            Console.WriteLine ("Actual dir:");
            Console.WriteLine (tmpDir);
            Console.WriteLine ("");

            Assert.AreEqual(expectedDir, tmpDir, "Paths don't match.");
        }
        
        
        [Test]
        public void Test_GetTmpDir_DeepWithinTmp()
        {
            var script = GetDummyScript();

            var timeStamp = TimeStamp;

            var tdc = script.TemporaryDirectoryCreator;
            /*new TemporaryDirectoryCreator(
                script.CurrentDirectory,
                timeStamp,
                true
            );*/

            var tmpDir = tdc.GetTmpDir();

            script.Relocate(tmpDir);

            tmpDir = tdc.GetTmpDir();

            Console.WriteLine ("");
            Console.WriteLine ("Tmp dir:");
            Console.WriteLine (tmpDir);
            Console.WriteLine ("");

            var expectedDir = Path.GetDirectoryName(WorkingDirectory)
                + Path.DirectorySeparatorChar
                + "csAnt.tmp"
                + Path.DirectorySeparatorChar
                //+ TimeStamp // TODO: Check if needed. There may be issues without it
                //+ Path.DirectorySeparatorChar
                //+ "csAnt.tmp"
                //+ Path.DirectorySeparatorChar
                + TimeStamp
                + Path.DirectorySeparatorChar
                + "csAnt";
            
            Console.WriteLine ("");
            Console.WriteLine ("Expected dir:");
            Console.WriteLine (expectedDir);
            Console.WriteLine ("Actual dir:");
            Console.WriteLine (tmpDir);
            Console.WriteLine ("");

            Assert.AreEqual(expectedDir, tmpDir, "Paths don't match.");
        }
    }
}

