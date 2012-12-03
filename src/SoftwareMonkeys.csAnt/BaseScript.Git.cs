using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void Git(params string[] arguments)
		{
			// TODO: Make this configurable
			var gitExe = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ "GitSharp"
				+ Path.DirectorySeparatorChar
				+ "Git.exe";

			StartDotNetExe(
				gitExe,
				arguments
			);
		}
	}
}

