using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void Error(string message)
		{
			IsError = true;
			
			Console.WriteLine ("");
			Console.WriteLine (GetIndentSpace() + "-------------------- !!! Error !!! --------------------");
			Console.WriteLine (GetIndentSpace() + "Script: " + ScriptName);
			Console.WriteLine (GetIndentSpace() + message);
			Console.WriteLine (GetIndentSpace() + "-------------------------------------------------------");
			Console.WriteLine ("");
		}
	}
}

