using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
	public class TestScriptReportGenerator
	{
		public ITestScript Script { get;set; }
        
        public TestScriptXmlReportFileNamer XmlReportFileNamer { get; set; }
        public TestScriptHtmlReportFileNamer HtmlReportFileNamer { get; set; }

        public TestScriptHtmlReportGenerator HtmlReportGenerator { get; set; }

        public BaseTestFixture TestFixture { get; set; }

		public TestScriptReportGenerator (
			ITestScript script,
            TestScriptXmlReportFileNamer xmlReportFileNamer,
            TestScriptHtmlReportFileNamer htmlReportFileNamer,
            TestScriptHtmlReportGenerator htmlReportGenerator,
            BaseTestFixture testFixture
		)
		{
			Script = script;
            XmlReportFileNamer = xmlReportFileNamer;
            HtmlReportFileNamer = htmlReportFileNamer;
            HtmlReportGenerator = htmlReportGenerator;
            TestFixture = testFixture;
		}

		public virtual void GenerateReports()
		{
            // TODO: Re-enable IsVerbose check
            //if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Generating test reports for scripts...");
                Console.WriteLine ("Original directory:");
                Console.WriteLine (Script.OriginalDirectory);
                Console.WriteLine ("Current directory:");
                Console.WriteLine (Script.CurrentDirectory);
            //}


			GenerateXmlReports();

			GenerateHtmlReports();
		}

		public virtual void GenerateXmlReports()
		{
            if (String.IsNullOrEmpty(Script.Console.Output))
                throw new Exception("The Script.Console.Output property is empty.");
			
			// TODO: Check if this should be injected
			var result = new DummyScriptResult(
				Script,
				!Script.IsError,
				Script.Console.Output,
				new DummyScriptResultSaver(
                    Script,
                    XmlReportFileNamer,
                    TestFixture
				)
			);

			result.Save();

		}

		public virtual void GenerateHtmlReports()
		{
			HtmlReportGenerator.Generate();
		}
	}
}

