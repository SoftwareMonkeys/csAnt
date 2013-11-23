using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    [TestFixture]
    public class ScriptXmlReportFileNamerTestFixture : BaseScriptingTestFixture
    {
        [Test]
        public void Test_GetXmlFileName_3Layers()
        {
            var script1 = GetTestScript("Test1");
            var script2 = GetTestScript("Test2", script1);
            var script3 = GetTestScript("Test3", script2);

            var scriptsResultsDir = script1.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results"
                + Path.DirectorySeparatorChar
                + "scripts";

            var fileNamer = new ScriptXmlResultFileNamer();

            var filePath = fileNamer.GetXmlFileName(script3);

            var expectedFilePath = scriptsResultsDir
                + Path.DirectorySeparatorChar
                + script1.TimeStamp
                + Path.DirectorySeparatorChar
                + "xml"
                + Path.DirectorySeparatorChar
                + script1.ScriptName
                + Path.DirectorySeparatorChar
                + script2.ScriptName
                + Path.DirectorySeparatorChar
                + script3.ScriptName
                + ".xml";
            
            Console.WriteLine ("Expected file path:");
            Console.WriteLine (expectedFilePath);
            Console.WriteLine ("Actual file path:");
            Console.WriteLine (filePath);

            Assert.AreEqual(expectedFilePath, filePath, "Wrong file path.");
        }
        
        [Test]
        public void Test_GetXmlFileName_1()
        {
            var script1 = GetTestScript("Test1");

            var fileNamer = new ScriptXmlResultFileNamer();

            var filePath = fileNamer.GetXmlFileName(script1);
            
            var resultsDir = script1.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results";

            var expectedFilePath = resultsDir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + script1.TimeStamp
                + Path.DirectorySeparatorChar
                + "xml"
                + Path.DirectorySeparatorChar
                + script1.ScriptName
                + ".xml";
            
            Console.WriteLine ("Expected file path:");
            Console.WriteLine (expectedFilePath);
            Console.WriteLine ("Actual file path:");
            Console.WriteLine (filePath);

            Assert.AreEqual(expectedFilePath, filePath, "Wrong file path.");
        }
    }
}

