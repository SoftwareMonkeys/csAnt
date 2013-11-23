using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    public class BaseScriptTestFixture : BaseTestFixture
    {
        public BaseScriptTestFixture ()
        {
        }
        
        public ScriptHtmlReportGenerator GetHtmlReportGenerator(ITestScript script)
        {
            var htmlReportFileNamer = new ScriptHtmlReportFileNamer();
            var xmlReportFileNamer = new ScriptXmlReportFileNamer();

            var generator = new ScriptHtmlReportGenerator(
                script,
                xmlReportFileNamer,
                htmlReportFileNamer
            );

            return generator;
        }

        public ScriptReportGenerator GetReportGenerator(ITestScript script)
        {
            var htmlGenerator = GetHtmlReportGenerator(script);

            var generator = new ScriptReportGenerator(
                script,
                htmlGenerator.XmlReportFileNamer,
                htmlGenerator.HtmlReportFileNamer,
                htmlGenerator,
                this
            );

            return generator;
        }
    }
}

