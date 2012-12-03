using System;
using System.Collections.Generic;
using System.IO;
using CSScriptLibrary;

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
			Console.WriteLine("");
			Console.WriteLine("-------------------- Executing script: " + scriptName + " --------------------");
			Console.WriteLine("");

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

			Console.WriteLine("");
			Console.WriteLine("-------------------- Finished script: " + scriptName + " --------------------");
			Console.WriteLine("");
		}

		public void ExecuteScriptFromFile(string scriptPath, string[] args)
		{			
			// Load the script assembly
			var a = CSScript.Load(scriptPath);

			// Get the script type
			var type = a.GetTypes()[0];

			// Create an instance of the script
			var script = a.CreateInstance(type.Name)
				.AlignToInterface<IScript>();

			script.Start(args);

			IsError = script.IsError;

			// TODO: Remove obsolete code

			/*// TODO: Check if there's a better way to format this path
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
			
			var process = Execute(command, argsString);

			IsError = process.ExitCode > 0;*/
		}
	}
}
