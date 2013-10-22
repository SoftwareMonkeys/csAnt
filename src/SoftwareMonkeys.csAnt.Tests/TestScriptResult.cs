using System;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Tests
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
			string log,
			TestScriptResultSaver saver
		)
		{
			Succeeded = succeeded;
			ScriptName = script.ScriptName;
			Script = script;
			Log = log;
			Saver = saver;
		}

		public void Save()
		{
			Saver.Save (this);
		}

	}
}

