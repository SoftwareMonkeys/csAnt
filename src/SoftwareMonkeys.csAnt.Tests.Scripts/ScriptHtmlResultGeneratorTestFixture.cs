using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    [TestFixture]
    public class ScriptHtmlReportGeneratorTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_GetHtmlReportFile()
        {
            var s = GetDummyScript();
            throw new NotImplementedException();
            /*var generator = GetHtmlReportGenerator(s);
            var htmlReportFileNamer = generator.HtmlReportFileNamer;
            var xmlReportFileNamer = generator.XmlReportFileNamer;

            var xmlFile = xmlReportFileNamer.GetXmlReportsDirectory(s)
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + s.TimeStamp
                + Path.DirectorySeparatorChar
                + "xml"
                + Path.DirectorySeparatorChar
                    + "Test_HelloWorld.xml";

            var f = generator.GetHtmlReportFile(xmlFile);

            var expectedHtmlFile = htmlReportFileNamer.GetHtmlReportsDirectory(s)
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
    }
}

