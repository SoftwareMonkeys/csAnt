using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		public void ExecuteScript(string scriptName)
		{
			ExecuteScript(scriptName, new string[]{});
		}
		
		public void ExecuteScript(string scriptName, string[] args)
		{
			string scriptFile = GetScriptPath(scriptName);

			if (!String.IsNullOrEmpty(scriptFile))
			{
				if (IsVerbose)
					Console.WriteLine("Executing script: " + scriptName);
			
				ExecuteScriptFromFile(scriptFile, args);
			}
			else
			{
				IsError = true;

				Console.WriteLine ("Cannot find '" + scriptName + "' script.");
			}
		}

		public void ExecuteScriptFromFile(string scriptPath, string[] args)
		{			
			// TODO: Check if there's a better way to format this path
			var cscsExe = Path.GetFullPath(
				CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ "cs-script"
				+ Path.DirectorySeparatorChar
				+ "cscs.exe"
			);

			if (IsVerbose)
				Console.WriteLine("cscs.exe file: " + cscsExe);
			
			string command = String.Empty;
			
			// If running on mono then the command is "mono"
			if (IsRunningOnMono())
			{
				command = "mono";
			}
			// Otherwise it's the cs-script file
			else
			{
				command = cscsExe;
			}

			if (IsVerbose)
				Console.WriteLine ("Command: " + command);
			
			var argsList = new List<string>();
			
			// If running on mono then the cs-script file path needs to be added as an argument
			if (IsRunningOnMono())
				argsList.Add("\"" + cscsExe + "\"");
			
			argsList.Add("\"" + scriptPath + "\"");
			
			argsList.AddRange(args);
			
			var argsString = String.Join(" ", argsList.ToArray());
			
			Execute(command, argsString);
		}
	}
}
