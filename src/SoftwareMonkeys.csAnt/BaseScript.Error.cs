using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void Error(string message)
		{
			IsError = true;
			
			Console.WriteLine ("");
			Console.WriteLine ("-------------------- !!! Error !!! --------------------");
			Console.WriteLine ("Script: " + ScriptName);
			Console.WriteLine (message);
			Console.WriteLine ("-------------------------------------------------------");
			Console.WriteLine ("");
		}
	}
}

