using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests.Unit
{
    [TestFixture]
    public class GetOriginalDirectoryTestFixture : BaseUnitTestFixture
    {
        [Test]
        public void Test_GetOriginalDirectory_WithinTmp()
        {
            var script = GetDummyScript();

            Console.WriteLine ("Current directory:");
            Console.WriteLine (script.CurrentDirectory);

            var originalDir = script.GetOriginalDirectory();

            var expected = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(script.CurrentDirectory)))
                + Path.DirectorySeparatorChar
                + "csAnt";

            Assert.AreEqual(expected, originalDir, "Paths don't match.");
        }
        
        [Test]
        public void Test_GetOriginalDirectory_DeepWithinTmp()
        {
            var script = GetDummyScript();

            script.CurrentDirectory = script.GetTmpDir ();

            Console.WriteLine ("Current directory:");
            Console.WriteLine (script.CurrentDirectory);

            var originalDir = script.GetOriginalDirectory();

            var expected = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(script.CurrentDirectory)))
                + Path.DirectorySeparatorChar
                + "csAnt";

            Assert.AreEqual(expected, originalDir, "Paths don't match.");
        }
        
        [Test]
        public void Test_GetOriginalDirectory_OutsideTmp()
        {
            var script = GetDummyScript();

            script.CurrentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(script.CurrentDirectory)))
                + Path.DirectorySeparatorChar
                    + "csAnt";

            Console.WriteLine ("Current directory:");
            Console.WriteLine (script.CurrentDirectory);

            var originalDir = script.GetOriginalDirectory();

            var expected = script.CurrentDirectory;

            Assert.AreEqual(expected, originalDir, "Paths don't match.");
        }
    }
}

