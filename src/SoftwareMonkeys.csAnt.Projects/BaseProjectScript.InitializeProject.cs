using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		/// <summary>
		/// Launches the initialization script in the specified external project directory.
		/// </summary>
		/// <param name='projectDirectory'>
		/// 
		/// </param>
		public void InitializeProject(string projectDirectory)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Initializing project:");
			Console.WriteLine (projectDirectory);
			Console.WriteLine ("");

			var initFile = "";
			if (!IsWindows)
				initFile = "init-" + ProjectName + "-project-linux.sh";
			else	
				initFile = "init-" + ProjectName + "-project-windows.sh";

			var cmdName = "bash";

			ProjectDirectory = projectDirectory;

			var initPath = projectDirectory
				+ Path.DirectorySeparatorChar
				+ initFile;

			Console.WriteLine("Command name:");
			Console.WriteLine(cmdName);
			Console.WriteLine("");
			Console.WriteLine("Initialize script launcher:");
			Console.WriteLine(initPath);

			if (File.Exists(initPath))
			{
				StartProcess(
					cmdName,
					"\"" + initPath + "\""
				);
			}
			else
			{
				Console.WriteLine("");
				Console.WriteLine("Can't find initialization file:");
				Console.WriteLine(initPath);
				Console.WriteLine("");
			}
		}
	}
}

