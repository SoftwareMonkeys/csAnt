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
			Console.WriteLine("// --------------------------------------------------");
			Console.WriteLine("// Executing script: " + scriptName);
			Console.WriteLine("// Path: " + CurrentDirectory);
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
			Console.WriteLine("// Finished executing script: " + scriptName);
			Console.WriteLine("// --------------------------------------------------");
			Console.WriteLine("");
		}

		public void ExecuteScriptFromFile (string scriptPath, string[] args)
		{			
			IScript script = GetScriptFromPath(scriptPath);

			// Get the original current directory
			var originalCurrentDirectory = script.CurrentDirectory;

			// Set the indent of the new script to be one more than the current script
			script.Indent = Indent+1;

			// Start the target script
			script.Start (args);

			// Ensure the script's current directory is reset back to its original value (in case it was modified in a script)
			script.CurrentDirectory = originalCurrentDirectory;

			// If the target script ran into an error then recognize that error in the current script
			IsError = script.IsError;

			Console.AppendOutput(script.Console.Output);

			// Add the summaries from the sub script to the outer script
			if (script.Summaries != null) {
				foreach (string summary in script.Summaries) {
					AddSummary (summary);
				}
			}
		}
	}
}
