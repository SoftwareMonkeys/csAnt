using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class TestReportGenerator
	{
		public ITestScript Script { get;set; }

		public TestReportGenerator (
			ITestScript script
		)
		{
			Script = script;
		}

		public virtual void GenerateReports()
		{
			GenerateXmlReports();

			GenerateHtmlReports();
		}

		public virtual void GenerateXmlReports()
		{
			var reportDir = GetReportsDirectory();

			if (Script.IsVerbose) {
				Console.WriteLine ("Report directory:");
				Console.WriteLine (reportDir);
			}
			
			// TODO: Check if this should be injected
			var result = new TestScriptResult(
				Script,
				!Script.IsError,
				Script.Console.Output,
				new TestScriptResultSaver(
					reportDir
				)
			);

			result.Save();

		}

		public virtual void GenerateHtmlReports()
		{
			var reportDir = GetReportsDirectory();

			// TODO: Check if this should be injected
			var generator = new TestHtmlReportGenerator(
				Script,
				reportDir
			);

			generator.Generate();
		}

		public virtual string GetReportsDirectory()
		{
			return Script.OriginalDirectory
				+ Path.DirectorySeparatorChar
				+ "tests"
				+ Path.DirectorySeparatorChar
				+ "results"
				+ Path.DirectorySeparatorChar
				+ "scripts";
		}
	}
}

