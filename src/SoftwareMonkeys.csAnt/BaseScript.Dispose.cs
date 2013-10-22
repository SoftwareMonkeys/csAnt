using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void Dispose ()
		{
			if (!IsVerbose) {
				Console.WriteLine ("Disposing '" + ScriptName + "' script.");
			}

			ClearTmp ();

			// TODO: Check if this is necessary
			foreach (var process in SubProcesses) {
				process.Kill();
			}

			Console.Dispose();

			if (IsError && StopOnFail)
				Environment.Exit(1);
		}
	}
}

