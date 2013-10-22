using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void SetUp()
		{
			Console.WriteLine ("Setting up '" + ScriptName + "' script.");
		}
	}
}

