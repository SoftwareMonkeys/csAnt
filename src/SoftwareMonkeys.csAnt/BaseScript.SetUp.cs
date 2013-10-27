using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void SetUp ()
		{
			if (IsVerbose) {
				Console.WriteLine ("");
				Console.WriteLine ("Setting up '" + ScriptName + "' script.");
				Console.WriteLine ("");
			}
		}
	}
}

