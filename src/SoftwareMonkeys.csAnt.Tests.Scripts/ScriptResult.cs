using System;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
	[Serializable]
	public class TestScriptResult
	{
		[XmlIgnore]
		public IScript Script { get;set; }

		public string ScriptName { get; set; }

		public bool Succeeded { get;set; }

		public string Log { get; set; }
		
		[XmlIgnore]
		public TestScriptResultSaver Saver { get; set; }
		
		public TestScriptResult ()
		{
		}

		public TestScriptResult (
			IScript script,
			bool succeeded,
			string logOutput,
			TestScriptResultSaver saver
		)
		{
            Console.WriteLine (logOutput);
            if (String.IsNullOrEmpty(script.ScriptName))
                throw new ArgumentException("The script.ScriptName property is empty.");

            if (String.IsNullOrEmpty(logOutput))
                throw new ArgumentException("The logOutput argument is empty.");

			Succeeded = succeeded;
			ScriptName = script.ScriptName;
			Script = script;
			Log = logOutput;
			Saver = saver;
		}

		public void Save()
		{
			Saver.Save (this);
		}

	}
}

