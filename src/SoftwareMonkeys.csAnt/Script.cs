using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.UI.csAntConsole
{
	/// <summary>
	/// A script that can be used programmatically.
	/// </summary>
	public class Script : BaseScript
	{
		public Script(string scriptName) : base(scriptName)
		{
		}

		public override bool Start (string[] args)
		{
			throw new System.NotImplementedException ();
		}
	}
}
