using System;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class TestSummarizer
	{
		public IScript Script { get; set; }

		public TestSummarizer (
			IScript script
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

