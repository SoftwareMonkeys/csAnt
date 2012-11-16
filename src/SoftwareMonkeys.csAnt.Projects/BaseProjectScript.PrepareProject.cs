using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		/// <summary>
		/// Launches the 'prepare-[linux|windows]' script in the specified external project directory.
		/// </summary>
		/// <param name='projectDirectory'>
		/// 
		/// </param>
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
			
			ProjectDirectory = projectDirectory;

			var preparePath = projectDirectory
				+ Path.DirectorySeparatorChar
				+ prepareFile;
			
			Console.WriteLine("Command name:");
			Console.WriteLine(cmdName);
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
				Console.WriteLine("");
				Console.WriteLine("Can't find prepare file:");
				Console.WriteLine(preparePath);
				Console.WriteLine("");
			}
		}
	}
}

