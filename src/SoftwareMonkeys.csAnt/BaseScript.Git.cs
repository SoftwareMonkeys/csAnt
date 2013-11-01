using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void Git(params string[] arguments)
		{
			// TODO: Clean up

			// TODO: Make this configurable
			/*var gitExe = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ "GitSharp"
				+ Path.DirectorySeparatorChar
				+ "Git.exe";
*/
			var gitExe = "git";

			StartProcess(
				gitExe,
				arguments
			);
		}
		
		public void GitDirectory(string workingDirectory, params string[] arguments)
		{
			// TODO: Clean up

			// TODO: Make this configurable
			/*var gitExe = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ "GitSharp"
				+ Path.DirectorySeparatorChar
				+ "Git.exe";
*/
			var originalDir = CurrentDirectory;

			CurrentDirectory = workingDirectory;

			var gitExe = "git";

			StartProcess(
				gitExe,
				arguments
			);

			CurrentDirectory = originalDir;
		}
	}
}

