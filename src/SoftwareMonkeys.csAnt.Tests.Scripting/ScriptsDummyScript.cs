using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
	public class ScriptsDummyScript : BaseDummyScript
	{
		public ScriptsDummyScript (string scriptName) : base(scriptName)
		{

		}

		public override bool Run (string[] args)
		{
			throw new System.NotImplementedException ();
		}
	}
}

