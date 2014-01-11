using System;
using NUnit.Framework;
using System.IO;
using CSScriptLibrary;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class NUnitTestRunnerTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_RunTests()
        {
            var script = GetDummyScript();
            
            var grabber = new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                );

            grabber.GrabOriginalFiles(
                "bin/**",
                "lib/NUnit/bin/**",
                "lib/NUnitResults/**"
            );

            CreateDummyTest();

            // Create another dummy script with a temporary directory as the original directory. This avoids having the dummy results being output
            // to the real original directory
            var script2 = GetDummyScript();
            script2.OriginalDirectory = script2.GetTmpDir();

            var runner = new NUnitTestRunner(
                script2,
                "Release"
            );

            var binDir = WorkingDirectory
                    + Path.DirectorySeparatorChar
                    + "bin"
                    + Path.DirectorySeparatorChar
                    + "Release";

            runner.RunTestsInDirectory(binDir, "SoftwareMonkeys.csAnt.Tests.CustomTestFixture");

            var htmlFileNamer = new HtmlResultFileNamer();
            
            var xmlFileNamer = new XmlResultFileNamer();

            var htmlResultsDir = htmlFileNamer.GetResultsDirectory(script2);

            var xmlResultsDir = xmlFileNamer.GetResultsDirectory(script2);

            Console.WriteLine ("Xml results dir:");
            Console.WriteLine (xmlResultsDir);
            Console.WriteLine ("Html results dir:");
            Console.WriteLine (htmlResultsDir);
            
            Assert.IsTrue (Directory.Exists(xmlResultsDir), "The xml results dir wasn't found.");

            Assert.IsTrue (Directory.Exists(htmlResultsDir), "The html results dir wasn't found.");
            
            Assert.AreEqual(1, Directory.GetFiles (xmlResultsDir).Length, "Wrong number of files found in xml results dir.");

            Assert.AreEqual(2, Directory.GetFiles (htmlResultsDir).Length, "Wrong number of files found in html results dir.");

            var finalHtmlResultsDir = htmlFileNamer.GetResultsReturnDirectory(script2);

            var finalXmlResultsDir = xmlFileNamer.GetResultsReturnDirectory(script2);

            Console.WriteLine ("Final xml results dir:");
            Console.WriteLine (finalXmlResultsDir);
            Console.WriteLine ("Final html results dir:");
            Console.WriteLine (finalHtmlResultsDir);
            
            Assert.IsTrue (Directory.Exists(finalXmlResultsDir), "The final xml results dir wasn't found.");

            Assert.IsTrue (Directory.Exists(finalHtmlResultsDir), "The final html results dir wasn't found.");
            
            Assert.AreEqual(1, Directory.GetFiles (finalXmlResultsDir).Length, "Wrong number of files found in final xml results dir.");

            Assert.AreEqual(2, Directory.GetFiles (finalHtmlResultsDir).Length, "Wrong number of files found in final html results dir.");
        }

        public void CreateDummyTest()
        {
            var testCode = GetTestCode();
        
            var assemblyFile = WorkingDirectory
                    + Path.DirectorySeparatorChar
                    + "bin"
                    + Path.DirectorySeparatorChar
                    + "Release"
                    + Path.DirectorySeparatorChar
                    + "CustomTestFixture"
                    + ".dll";

            if (!Directory.Exists (Path.GetDirectoryName(assemblyFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(assemblyFile));
                            
            CSScript.CompileCode(testCode, assemblyFile, true, new string[]{});
        }

        public string GetTestCode()
        {
            return @"using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class CustomTestFixture
    {
        public CustomTestFixture()
        {}

        [Test]
        public void CustomTest()
        {
            Console.WriteLine(""Hello world"");

            Assert.IsTrue(true);
        }
    }
}
";
        }
    }
}

