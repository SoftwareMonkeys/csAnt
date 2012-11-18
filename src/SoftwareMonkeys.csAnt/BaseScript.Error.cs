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
			Console.WriteLine (message);
			Console.WriteLine ("-------------------------------------------------------");
			Console.WriteLine ("");

			if (StopOnFail)
				Environment.Exit(1);
		}
	}
}

