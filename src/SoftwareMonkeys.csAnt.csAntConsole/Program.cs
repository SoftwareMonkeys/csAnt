using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.UI.csAntConsole
{
	class Program
	{
        static public ConsoleWriter ConsoleWriter { get;set; }

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
            ConsoleWriter = new ConsoleWriter(Console.Out, "logs", scriptName);
			Console.SetOut (ConsoleWriter);
		}

		static public void Execute (string scriptName, string[] args)
        {
            InitializeConsoleWriter (scriptName);
            
            var arguments = new Arguments (args);

            var script = new Script (scriptName);

            script.Indent = -1; // Set the launcher script indent to -1 so it doesn't increase the indent of the script it calls

            script.ConsoleWriter = ConsoleWriter;

            if (arguments.Contains ("v")
                || arguments.Contains ("verbose"))
                script.IsVerbose = true;

            if (script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Launching script...");
                Console.WriteLine ("Script name:");
                Console.WriteLine (scriptName);
                Console.WriteLine ("");
            }

            var startTime = script.Time;

            if (arguments.Contains ("b"))
                script.CurrentDirectory = Path.GetFullPath (arguments ["b"]);

            if (arguments.Contains ("t")) {
                script.Time = DateTime.Parse (arguments ["t"]);
                script.TimeStamp = arguments ["t"];
            }

			if (arguments.Contains ("dbg")
			    || arguments.Contains ("debug"))
				script.IsDebug = true;

			if (script.IsVerbose) {
				Console.WriteLine ("");
				Console.WriteLine ("Base directory:");
				Console.WriteLine (script.CurrentDirectory);
				Console.WriteLine ("");
			}
			
			script.CurrentDirectory = Path.GetFullPath(
				Environment.CurrentDirectory
			);
            
            InitializeComponents(script);


			if (String.IsNullOrEmpty(scriptName))
			{
				Console.WriteLine("");
				Console.WriteLine("Can't find script '" + scriptName + "'.");
				Console.WriteLine("");
			}
			else
			{			
				Environment.CurrentDirectory = script.CurrentDirectory;
			
                script.SetUp();

                try
                {
				    // Execute the script
				    script.ExecuteScript(scriptName, args);
                }
                catch(ScriptNotFoundException ex)
                {
                    script.Error ("Cannot find '" + ex.ScriptName + "' script.\nCurrent directory: " + script.CurrentDirectory + "\n" + ex.ToString());
                }

                script.TearDown();

				// Calculate the amount of time the script took to run
				var totalTime = DateTime.Now.Subtract(startTime);

				if (script.IsVerbose)
				{
					// Output the summaries to help the user see what happened
					script.OutputSummaries();

					Console.WriteLine("");
					Console.WriteLine(script.Indenter.GetIndentSpace() + "// Duration: " + totalTime.ToString());
				}

				if (script.IsError)
					Console.WriteLine(script.Indenter.GetIndentSpace() + "// !!!!!!!!!!!!!!!!!!!!  Failed  !!!!!!!!!!!!!!!!!!!!");
				else
					Console.WriteLine(script.Indenter.GetIndentSpace() + "// ==================== Success! ====================");


				Console.WriteLine("");
			}
		}

        static public void InitializeComponents(IScript script)
        {
            // TODO: Remove obsolete code
            /*script.InitializePackageManager(
                new PackageManager()
            );*/

            script.InitializeFileFinder(
                new FileFinder(script.IsVerbose)
            );
        }
	}
}