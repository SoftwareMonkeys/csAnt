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

		// TODO: Check if needed
		/*static public string GetOutputDirectory()
		{
			var output = Path.GetDirectoryName(
				Assembly.GetExecutingAssembly().FullName
				);

			output = Path.GetFullPath(
				output
				+ "/../"
				+ "scriptlogs"
			);

			return output;
		}*/

		static public void Execute (string scriptName, string[] args)
		{
			InitializeConsoleWriter (scriptName);

			var startTime = DateTime.Now;

			var parser = new Arguments (args);

			var scr = new Script (scriptName);

			scr.Console = Console;

			if (parser.Contains ("b"))
				scr.CurrentDirectory = Path.GetFullPath (parser ["b"]);

			if (scr.IsVerbose) {
				Console.WriteLine ("");
				Console.WriteLine ("Base directory:");
				Console.WriteLine (scr.CurrentDirectory);
				Console.WriteLine ("");
			}

			scr.IsVerbose = parser.Contains("verbose");
			
			scr.CurrentDirectory = Path.GetFullPath(
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
				Environment.CurrentDirectory = scr.CurrentDirectory;
			
				// Execute the script
				scr.ExecuteScript(scriptName, args);

				// Calculate the amount of time the script took to run
				var totalTime = DateTime.Now.Subtract(startTime);


				if (scr.IsVerbose)
				{
					// Output the summaries to help the user see what happened
					scr.OutputSummaries();

					Console.WriteLine("");
					Console.WriteLine("Duration: " + totalTime.ToString());
					Console.WriteLine("Successful: " + !scr.IsError);
					Console.WriteLine("");
				}

				if (scr.IsError)
					Console.WriteLine("// !!!!!!!!!!!!!!!!!!!!  Failed  !!!!!!!!!!!!!!!!!!!!");
				else
					Console.WriteLine("// ==================== Success! ====================");


				Console.WriteLine("");
				Console.WriteLine("");
			}
		}
	}
}