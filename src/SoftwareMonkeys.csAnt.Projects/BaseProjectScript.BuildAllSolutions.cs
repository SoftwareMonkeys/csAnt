using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void BuildAllSolutions(string directory)
		{
			BuildAllSolutions(directory, "Release");
		}

		public void BuildAllSolutions(string directory, string mode)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Building all solutions...");
			Console.WriteLine ("");
			Console.WriteLine ("Mode: " + mode);
			Console.WriteLine ("");

			// Don't stop on fail
			StopOnFail = false;

			int successful = 0;
			int failed = 0;
			int total = 0;

			foreach (string slnFile in Directory.GetFiles(directory, "*.sln", SearchOption.AllDirectories))
			{
				if (!isError)
				{
					total++;

					if (BuildSolution(slnFile, mode))
						successful++;
					else
					{
						failed++;
						isError = true;
					}
				}

				if (isError)
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
			
			// Enable stopping on failure again
			StopOnFail = true;

			if (failed > 0)
				Error(failed + " solution(s) failed to build.");
		}
	}
}

