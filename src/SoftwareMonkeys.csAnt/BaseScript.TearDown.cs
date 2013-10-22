using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void TearDown()
		{
			Console.WriteLine ("Tearing down '" + ScriptName + "' script.");
		}
	}
}

