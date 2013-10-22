using System;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class TestSummarizer
	{
		public ITestScript Script { get; set; }

		public TestSummarizer (
			ITestScript script
		)
		{
			Script = script;
		}

		public void Summarize()
		{
			if (Script.IsError)
				Script.AddSummary ("* " + Script.ScriptName + " test script failed.");
			else
				Script.AddSummary (Script.ScriptName + " test script succeeded.");

		}
	}
}

