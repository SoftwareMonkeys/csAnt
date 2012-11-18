using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt.UI.csAntConsole
{
	class Program
	{
		public static void Main(string[] args)
		{			
			Console.WriteLine("");

			// Get the script name from the first argument
			string scriptName = args[0];
			
			var argsList = new List<string>(args);

			// Remove the first argument (as it's the script name)
			if (argsList.Count > 0)
				argsList.RemoveAt(0);

			// Execute the script
			Execute(scriptName, argsList.ToArray());
		}

		static public void Execute(string scriptName, string[] args)
		{
			var startTime = DateTime.Now;

			Console.WriteLine("");
			Console.WriteLine("============================================");
			Console.WriteLine("Executing script: " + scriptName);
			Console.WriteLine("============================================");
			Console.WriteLine("");
			
			var scr = new LauncherScript();

			scr.IsVerbose = new Arguments(args).Contains("verbose");
			
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
				
				Console.WriteLine("");
				Console.WriteLine("Total execution time: " + totalTime.ToString());

				if (!scr.IsError)
					Console.WriteLine("---------- Finished ----------");
				else
					Console.WriteLine("!!!!!!!!!! Failed !!!!!!!!!!");

				Console.WriteLine("");
			}
		}
	}
}