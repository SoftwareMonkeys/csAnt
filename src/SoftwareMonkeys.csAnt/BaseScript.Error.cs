using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
        public void Error (string message, Exception ex)
        {
            Error (message + Environment.NewLine + ex.ToString());
        }

        public void Error (Exception ex)
        {
            Error(ex.ToString());
        }

		public void Error(string message)
		{
			IsError = true;
			
			Console.WriteLine ("");
			Console.WriteLine (GetIndentSpace() + "// -------------------- !!! Error !!! --------------------");
			Console.WriteLine (GetIndentSpace() + "// Script: " + ScriptName);
            WriteScriptStack(GetScriptStack());
			Console.WriteLine (GetIndentSpace() + message);
			Console.WriteLine (GetIndentSpace() + "// -------------------------------------------------------");
			Console.WriteLine ("");

            AddSummary("Error in script '" + ScriptName + "': " + message);

            if (StopOnFail)
                Environment.Exit(0);
		}
	}
}

