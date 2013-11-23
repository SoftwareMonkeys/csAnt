using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void ClearTmp ()
        {
            var tmpRoot = GetTmpRoot ();

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Clearing temporary directory...");
                Console.WriteLine (tmpRoot);
                Console.WriteLine ("");
            }

            if (Directory.Exists (tmpRoot))
                Directory.Delete (tmpRoot, true);
            else {
                if (IsVerbose)
                {
                    Console.WriteLine ("Temporary directory not found. Skipping.");
                    Console.WriteLine ("");
                }
            }

            AddSummary("Cleared all temporary files and folders from: " + tmpRoot);
		}
	}
}

