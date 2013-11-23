using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    public class TestScriptConstructor
    {
        public void Construct(ITestScript script)
        {
            script.IsVerbose = true;
            script.StopOnFail = false;
            
            script.Utilities =  new TestUtilities(script);

            var xmlFileNamer = new TestScriptXmlReportFileNamer();

            var htmlFileNamer = new TestScriptHtmlReportFileNamer();
            var generator = new TestScriptReportGenerator(
                script,
                xmlFileNamer,
                htmlFileNamer,
                new TestScriptHtmlReportGenerator(
                    script,
                    xmlFileNamer,
                    htmlFileNamer
                )
            );

            // TODO: Check if these should be injected
            script.ReportGenerator = generator;

            script.TestSummarizer = new TestSummarizer(script);
            
            script.Grabber = new TestFilesGrabber(script);
            
            script.Relocator = new TestRelocator(script);

            script.SetUpper = new TestScriptSetUpper();

            script.TearDowner = new TestScriptTearDowner();
        }
    }
}

