using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SoftwareMonkeys.csAnt.UI.csAntConsole
{
	class Program
	{
		static public ConsoleWriter Console { get; set; }

		public static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				// Get the script name from the first argument
				string scriptName = args[0];
				
				var argsList = new List<string>(args);

				// Remove the first argument (as it's the script name)
				if (argsList.Count > 0)
					argsList.RemoveAt(0);

				// Execute the script
				Execute(scriptName, argsList.ToArray());
			}
			else
			{
				Console.WriteLine ("Please specify the name of the script as an argument.");
			}
		}

		static public void InitializeConsoleWriter(string scriptName)
		{	
			Console = new ConsoleWriter("logs", scriptName);
		}

		static public void Execute (string scriptName, string[] args)
		{
			InitializeConsoleWriter (scriptName);

			var startTime = DateTime.Now;

			var arguments = new Arguments (args);

			var script = new Script (scriptName);

			script.Console = Console;

			if (arguments.Contains ("b"))
				script.CurrentDirectory = Path.GetFullPath (arguments ["b"]);
			
			if (arguments.Contains ("v")
			    || arguments.Contains ("verbose"))
				script.IsVerbose = true;

			if (script.IsVerbose) {
				Console.WriteLine ("");
				Console.WriteLine ("Base directory:");
				Console.WriteLine (script.CurrentDirectory);
				Console.WriteLine ("");
			}

			script.IsVerbose = arguments.Contains("verbose");
			
			script.CurrentDirectory = Path.GetFullPath(
				Environment.CurrentDirectory
			);

			if (String.IsNullOrEmpty(scriptName))
			{
				Console.WriteLine("");
				Console.WriteLine("Can't find script '" + scriptName + "'.");
				Console.WriteLine("");
			}
			else
			{			
				Environment.CurrentDirectory = script.CurrentDirectory;
			
				// Execute the script
				script.ExecuteScript(scriptName, args);

				// Calculate the amount of time the script took to run
				var totalTime = DateTime.Now.Subtract(startTime);


				if (script.IsVerbose)
				{
					// Output the summaries to help the user see what happened
					script.OutputSummaries();

					Console.WriteLine("");
					Console.WriteLine("Duration: " + totalTime.ToString());
					Console.WriteLine("Successful: " + !script.IsError);
					Console.WriteLine("");
				}

				if (script.IsError)
					Console.WriteLine("// !!!!!!!!!!!!!!!!!!!!  Failed  !!!!!!!!!!!!!!!!!!!!");
				else
					Console.WriteLine("// ==================== Success! ====================");


				Console.WriteLine("");
				Console.WriteLine("");
			}
		}
	}
}