using System;
using System.Collections.Generic;
using System.IO;
using CSScriptLibrary;
using System.Reflection;

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
		
		public void ExecuteScript(string scriptName, params string[] args)
		{
			Console.WriteLine("");
			Console.WriteLine("---------- Executing script: " + scriptName + " ----------");
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
			Console.WriteLine("---------- Finished script: " + scriptName + " ----------");
			Console.WriteLine("");
		}

		public void ExecuteScriptFromFile(string scriptPath, string[] args)
		{			
			// Load the script assembly
			Assembly a = CSScript.Load(scriptPath);

			// Get the script type
			var type = a.GetTypes()[0];

			var b = (IScript)a
				.CreateInstance(type.Name);

			// Create an instance of the script
			IScript script = b
				;//.AlignToInterface<IScript>();

			// Get the original current directory
			var originalCurrentDirectory = script.CurrentDirectory;

			// Start the target script
			script.Start(args);

			// Ensure the script's current directory is reset back to its original value (in case it was modified in a script)
			script.CurrentDirectory = originalCurrentDirectory;

			// If the target script ran into an error then recognize that error in the current script
			IsError = script.IsError;
		}
	}
}
