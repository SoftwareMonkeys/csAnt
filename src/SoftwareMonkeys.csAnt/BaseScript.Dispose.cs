using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void Dispose ()
		{
			if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Disposing '" + ScriptName + "' script.");
                Console.WriteLine ("");
			}

            TearDown();

			ClearTmp ();

			// TODO: Check if this is necessary
			foreach (var process in SubProcesses) {
				process.Kill();
			}

			if (IsError && StopOnFail)
				Environment.Exit(1); // TODO: Check if needed
		}
	}
}

