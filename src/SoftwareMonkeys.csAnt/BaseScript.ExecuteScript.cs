using System;
using System.Collections.Generic;
using System.IO;
using CSScriptLibrary;
using System.Reflection;
using System.Text;

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
			var parentScriptList = GetParentScriptList();

			Console.WriteLine("");
			Console.WriteLine(GetIndentSpace(Indent) + "// --------------------------------------------------");
			Console.WriteLine(GetIndentSpace(Indent) + "// Executing script: " + scriptName);
			WriteScriptStack(parentScriptList);
			Console.WriteLine(GetIndentSpace(Indent) + "// Directory: " + CurrentDirectory);
			Console.WriteLine("");

			string scriptFile = GetScriptPath(scriptName);

			if (!String.IsNullOrEmpty(scriptFile))
			{
				if (IsVerbose)
				{
					Console.WriteLine(GetIndentSpace(Indent) + "Script file:");
					Console.WriteLine(GetIndentSpace(Indent) + scriptFile);
				}
			
				ExecuteScriptFromFile(scriptFile, args);
			}
			else
			{
				IsError = true;

				Console.WriteLine ("Cannot find '" + scriptName + "' script.");
			}
			
			Console.WriteLine("");
			Console.WriteLine(GetIndentSpace(Indent) + "// Finished executing script: " + scriptName);
			WriteScriptStack(parentScriptList);
			Console.WriteLine(GetIndentSpace(Indent) + "// --------------------------------------------------");

		}

		public void WriteScriptStack (string[] list)
		{
			var builder = new StringBuilder();

			if (list.Length > 0) {
				builder.Append(GetIndentSpace(Indent) + "// Script stack: ");

				for (var i = 0; i < list.Length; i++) {
						if (i > 0)
							builder.Append(", ");

						if (String.IsNullOrEmpty(list[i]))
							throw new Exception("Item is null or empty.");

						builder.Append(" ^ " + list [i]);
				}
			
				builder.Append(Environment.NewLine);

				Console.Write(builder.ToString());
			}
		}

		public string[] GetParentScriptList ()
		{
			var c = Console;

			List<string> list = new List<string> ();

			if (c is SubConsoleWriter) {
				while (c is SubConsoleWriter) {
					c = ((SubConsoleWriter)c).ParentWriter;
					//if (!list.Contains(c.ScriptName))
						list.Insert (0, c.ScriptName);
				}
			}

			return list.ToArray ();
		}

		public void ExecuteScriptFromFile (string scriptPath, string[] args)
		{			
			IScript script = ActivateScript (scriptPath);

			// Get the original current directory
			var originalCurrentDirectory = script.CurrentDirectory;

			// Set the indent of the new script to be one more than the current script
			script.Indent = Indent + 1;

			script.SetUp (); // TODO: Check if this should be automatically run by the script itself
		
			// Give the sub script the same time stamp (so all sub scripts can be marked with the same time, helping to organize them)
			script.TimeStamp = TimeStamp;	// Start the target script

			// Run the Start function inside a try...catch block 
			try {
				script.Start (args);
			} catch (Exception ex) {
				script.Error (ex.ToString());
			}
			finally{
				script.TearDown (); // TODO: Check if this should be automatically run by the script itself
			}

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
