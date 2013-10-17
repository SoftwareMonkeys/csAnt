using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void BuildAllSolutions(string directory)
		{
			BuildAllSolutions(directory, "Release");
		}

		public void BuildAllSolutions (string directory, string mode)
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

			List<string> failedSolutions = new List<string> ();

			foreach (string slnFile in Directory.GetFiles(directory, "*.sln", SearchOption.AllDirectories)) {
				if (!isError) {
					total++;

					if (BuildSolution (slnFile, mode))
						successful++;
					else {
						failed++;
						isError = true;
					}
				}

				if (isError) {
					failedSolutions.Add (slnFile);
					break;
				}
			}
			
			Console.WriteLine ("");
			Console.WriteLine ("Build finished!");
			Console.WriteLine ("");
			Console.WriteLine ("Total: " + total);
			Console.WriteLine ("Successful: " + successful);
			Console.WriteLine ("Failed: " + failed);
			Console.WriteLine ("");

			if (failedSolutions.Count > 0) {
				Console.WriteLine ("The following solutions failed to build:");
				foreach (string failedSolution in failedSolutions) {
					Console.WriteLine ("  " + failedSolution.Replace (ProjectDirectory, ""));

					AddSummary("Failed to build: " + failedSolution.Replace (ProjectDirectory, ""));
				}
			}
			
			Console.WriteLine ("");

			
			// Enable stopping on failure again
			StopOnFail = true;

			if (failed > 0)
				Error(failed + " solution(s) failed to build.");
		}
	}
}

