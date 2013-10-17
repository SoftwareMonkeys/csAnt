using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void Dispose ()
		{
			ClearTmp ();

			foreach (var process in SubProcesses) {
				process.Kill();
			}

			Console.Dispose();
		}
	}
}

