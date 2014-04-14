using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.BuildEngine;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseProjectScript
	{
		public bool BuildSolution(
			string solutionFilePath
		)
		{
			return BuildSolution (
				solutionFilePath,
				"Release"
			);
		}

		public bool BuildSolution(
			string solutionFilePath,
			string mode
		)
		{
			var success = false;

            var solutionBuilder = new SolutionBuilder(mode);
            success = solutionBuilder.BuildSolutionFile(solutionFilePath);

			if (!success)
				isError = true;
			else
				AddSummary("Built solution: " + Path.GetFileName(solutionFilePath));

			return success;
		}
		
	}
}
