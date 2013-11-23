using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    [TestFixture]
    public class TestScriptReportGeneratorTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_GenerateReports()
        {
            var script = GetTestScript("Test_BasicDeployAndTest");

            var script2 = GetTestScript("Test_HelloWorld");

            script2.ParentScript = script;

            script.Console.Output = "Test output";

            CreateTestFile1(script);

            CreateTestFile2(script2);

            var generator = GetReportGenerator(script);

            generator.GenerateReports();

            var resultsDir = script.CurrentDirectory
                + "tests"
                + Path.DirectorySeparatorChar
                    + "results";

            Console.WriteLine ("Results dir:");
            Console.WriteLine (resultsDir);

            var htmlFile1Path = resultsDir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "html"
                + Path.DirectorySeparatorChar
                + "Test_BasicDeployAndTest.html";
            
            Console.WriteLine ("");
            Console.WriteLine ("Looking for HTML file 1 at:");
            Console.WriteLine (htmlFile1Path);
            Console.WriteLine ("");

            Assert.IsTrue(File.Exists(htmlFile1Path), "Html file 1 not found.");
            
            var htmlIndexFile1Path = resultsDir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "html"
                + Path.DirectorySeparatorChar
                + "index.html";
            
            Console.WriteLine ("");
            Console.WriteLine ("Looking for HTML index file 1 at:");
            Console.WriteLine (htmlIndexFile1Path);
            Console.WriteLine ("");

            Assert.IsTrue(File.Exists(htmlIndexFile1Path), "Index file 1 not found.");
            
            var htmlFile2Path = resultsDir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "html"
                + Path.DirectorySeparatorChar
                + "Test_BasicDeployAndTest"
                + Path.DirectorySeparatorChar
                + "Test_HelloWorld.html";

            Assert.IsTrue(File.Exists(htmlFile2Path), "Html file 2 not found.");
            
            var htmlIndexFile2Path = resultsDir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "html"
                + Path.DirectorySeparatorChar
                + "Test_BasicDeployAndTest"
                + Path.DirectorySeparatorChar
                + "index.html";

            Assert.IsTrue(File.Exists(htmlIndexFile2Path), "Index file 2 not found.");
            /*var xmlFile1Path = reportsDir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + s.TimeStamp
                + Path.DirectorySeparatorChar
                + "xml"
                + Path.DirectorySeparatorChar
                + "Test_BasicDeployAndTest.xml";

            var xmlFile1Content = ;

            var xmlFile2Path = reportsDir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + s.TimeStamp
                + Path.DirectorySeparatorChar
                + "xml"
                + Path.DirectorySeparatorChar
                + "Test_BasicDeployAndTest
                + Path.DirectorySeparatorChar
                + "Test_HelloWorld.xml";

            var v = new DummyScriptHtmlReportGenerator(s, reportsDir);

            var f = v.GetHtmlReportFile(xmlFile);

            var expectedHtmlFile = reportsDir
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + s.TimeStamp
                + Path.DirectorySeparatorChar
                + "html"
                + Path.DirectorySeparatorChar
                + "Test_HelloWorld.html";

            Console.WriteLine ("Expected file path:");
            Console.WriteLine (expectedHtmlFile);
            Console.WriteLine ("File path:");
            Console.WriteLine (f);

            Assert.AreEqual(expectedHtmlFile, f);*/
        }

        public void CreateTestFile1(ITestScript script)
        {
            var result = new DummyScriptResult(
                script,
                true,
                "Test log output",
                new DummyScriptResultSaver(script, new DummyScriptXmlReportFileNamer(), this)
            );

            result.Save();
        }
        

        public void CreateTestFile2(ITestScript script)
        {
            var result = new DummyScriptResult(
                script,
                true,
                "Test log output",
                new DummyScriptResultSaver(script, new DummyScriptXmlReportFileNamer(), this)
            );

            result.Save();
        }

       /* public void CreateIndexFile1(string reportsDir)
        {
            var c = @"﻿<html>
    <head>
        <title>
            Test Script Reports
        </title>
        <link href='{{ProjectDirectory}}/styles/general.css' rel='stylesheet' type='text/css'>
    </head>
    <body>
        <h1>Test Script Reports</h1>
        <table id='ReportsTable' width='100%'>
        <tr id=""Test_BasicDeployAndTest""><td><a href=""Test_BasicDeployAndTest.html"">Test_BasicDeployAndTest</a></td><td>Succeeded: <span style='color: green;'>True</span></td></tr><tr id=""Test_BasicDeployAndTest""><td><a href=""Test_BasicDeployAndTest.html"">Test_BasicDeployAndTest</a></td><td>Succeeded: <span style='color: green;'>True</span></td><td><a href='Test_BasicDeployAndTest/index.html'>Sub Tests</a></td></tr></table>
    </body>
</html>";

            var f = reportsDir
                + Path.DirectorySeparatorChar
                + "Index.html";
        }
        
        public void CreateIndexFile2()
        {
            var c = @"﻿<html>
    <head>
        <title>
            Test Script Reports
        </title>
        <link href='{{ProjectDirectory}}/styles/general.css' rel='stylesheet' type='text/css'>
    </head>
    <body>
        <h1>Test Script Reports</h1>
        <table id='ReportsTable' width='100%'>
        <tr id=""Test_HelloWorld""><td><a href=""Test_HelloWorld.html"">Test_BasicDeployAndTest</a></td><td>Succeeded: <span style='color: green;'>True</span></td></tr><tr id=""Test_HelloWorld""><td><a href=""Test_HelloWorld.html"">Test_HelloWorld</a></td><td>Succeeded: <span style='color: green;'>True</span></td><td><a href='Test_HelloWorld/index.html'>Sub Tests</a></td></tr></table>
    </body>
</html>";
        }*/
    }
}

