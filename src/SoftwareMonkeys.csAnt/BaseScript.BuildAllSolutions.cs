using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void BuildAllSolutions(string directory)
		{
			// Don't stop on fail
			StopOnFail = false;

			int successful = 0;
			int failed = 0;
			int total = 0;

			foreach (string slnFile in Directory.GetFiles(directory, "*.sln", SearchOption.AllDirectories))
			{
				if (!IsError)
				{
					total++;

					if (BuildSolution(slnFile))
						successful++;
					else
					{
						failed++;
						IsError = true;
					}
				}

				if (IsError)
					break;
			}
			
			Console.WriteLine ("");
			Console.WriteLine ("Build finished!");
			Console.WriteLine ("");
			Console.WriteLine ("Total: " + total);
			Console.WriteLine ("Successful: " + successful);
			Console.WriteLine ("Failed: " + failed);
			Console.WriteLine ("");
			Console.WriteLine ("");
			
			// Go back to stopping on failure
			StopOnFail = true;

			if (failed > 0)
				Error(failed + " solution(s) failed to build.");
		}
	}
}

