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
		public bool BuildSolution(
			string solutionFilePath
		)
		{
			var success = false;

			if (IsRunningOnMono())
			{
				success = RunXBuild(solutionFilePath);
			}
			else
			{
				success = RunMicrosoftBuild(solutionFilePath);
			}

			if (!success)
				IsError = true;

			return success;
		}
		
		public static bool IsRunningOnMono ()
		{
		    return Type.GetType("Mono.Runtime") != null;
		}

		public bool RunMicrosoftBuild(string solutionFilePath)
		{
			var success = false;

			// Instantiate a new Engine object
			Engine engine = new Engine();

			// Point to the path that contains the .NET Framework 4.0 CLR and tools
			engine.BinPath = @"c:\windows\microsoft.net\framework\v4.0.xxxxx";

			// Instantiate a new console logger
			ConsoleLogger logger = new ConsoleLogger();

			// Register the logger with the engine
			engine.RegisterLogger(logger);

			// Build a project file
			success = engine.BuildProjectFile(solutionFilePath);

			//Unregister all loggers to close the log file
			engine.UnregisterAllLoggers();

			if (success)
				Console.WriteLine("Build succeeded.");
			else
			{
				IsError = true;
				Console.WriteLine(@"Build failed. View C:\temp\build.log for details");
			}

			return success;
		}
		
		public bool RunXBuild(string solutionFilePath)
		{
			var cmdName = "xbuild";

			var arguments = new string[]{
				"\"" + solutionFilePath + "\""
			};

			var process = StartProcess(
				cmdName,
				arguments
			);

			var success = (process.ExitCode == 0);

			if (success)
            	Console.WriteLine("Build succeeded.");
			else
			{
				IsError = true;

            	Console.WriteLine("Build failed.");
			}

			if (!success && StopOnFail)
			{
				Error ("Build failed.");
			}

			return success;
		}
		
	}
}
