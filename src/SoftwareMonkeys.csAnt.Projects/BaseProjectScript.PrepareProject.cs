using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void PrepareProject(string projectDirectory)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Preparing project:");
			Console.WriteLine (projectDirectory);
			Console.WriteLine ("");

			var prepareFile = "prepare-linux.sh";

			var cmdName = "bash";

			if (IsWindows)
			{
				prepareFile = "prepare-windows.vbs";
				cmdName = "cscript";
			}
			
			ProjectDirectory = tmpDir;

			var preparePath = GetNewestFolder(tmpDir)
				+ Path.DirectorySeparatorChar
				+ prepareFile;
			
			Console.WriteLine("Command name:");
			Console.WriteLine(preparePath);
			Console.WriteLine("");
			Console.WriteLine("Prepare script launcher:");
			Console.WriteLine(preparePath);

			if (File.Exists(preparePath))
			{
				StartProcess(
					cmdName,
					"\"" + preparePath + "\""
				);
			}
			else
			{
				Console.WriteLine("Can't find '" + prepareFile + "' file in:");
				Console.WriteLine(projectDirectory);
			}
		}
	}
}

