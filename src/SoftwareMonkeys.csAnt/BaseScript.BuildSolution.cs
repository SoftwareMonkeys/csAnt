using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.BuildEngine;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		public void BuildSolution(string solutionFilePath)
		{
			if (IsRunningOnMono())
			{
				RunXBuild(solutionFilePath);
			}
			else
			{
				RunMicrosoftBuild(solutionFilePath);
			}
			
		}
		
		public static bool IsRunningOnMono ()
		{
		    return Type.GetType("Mono.Runtime") != null;
		}

		public void RunMicrosoftBuild(string solutionFilePath)
		{
			// Instantiate a new Engine object
			Engine engine = new Engine();

			// Point to the path that contains the .NET Framework 4.0 CLR and tools
			engine.BinPath = @"c:\windows\microsoft.net\framework\v4.0.xxxxx";

			// Instantiate a new console logger
			ConsoleLogger logger = new ConsoleLogger();

			// Register the logger with the engine
			engine.RegisterLogger(logger);

			// Build a project file
			bool success = engine.BuildProjectFile(solutionFilePath);

			//Unregister all loggers to close the log file
			engine.UnregisterAllLoggers();

			if (success)
				Console.WriteLine("Build succeeded.");
			else
				Console.WriteLine(@"Build failed. View C:\temp\build.log for details");
		}
		
		public void RunXBuild(string solutionFilePath)
		{
			var cmdName = "xbuild";

			var arguments = new string[]{
				"\"" + solutionFilePath + "\""
			};

			Execute(
				cmdName,
				arguments
			);
			
            		Console.WriteLine("Build succeeded.");
		}
		
	}
}
