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
            new SolutionBuilder().BuildAllSolutions(directory, mode);
		}
	}
}

