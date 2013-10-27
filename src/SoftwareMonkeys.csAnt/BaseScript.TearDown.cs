using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void TearDown ()
		{
			if (IsVerbose) {
				Console.WriteLine ("");
				Console.WriteLine ("Tearing down '" + ScriptName + "' script.");
				Console.WriteLine ("");
			}
		}
	}
}

