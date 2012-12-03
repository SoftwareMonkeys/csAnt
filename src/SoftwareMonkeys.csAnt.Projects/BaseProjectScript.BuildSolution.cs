using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.BuildEngine;

namespace SoftwareMonkeys.csAnt.Projects
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseProjectScript
	{
		public bool BuildSolution(
			string solutionFilePath
		)
		{
			return BuildSolution (
				solutionFilePath,
				"Release"
			);
		}

		public bool BuildSolution(
			string solutionFilePath,
			string mode
		)
		{
			var success = false;

			if (IsMono)
			{
				success = RunXBuild(solutionFilePath, mode);
			}
			else
			{
				success = RunMicrosoftBuild(solutionFilePath, mode);
			}

			if (!success)
				isError = true;

			return success;
		}

		public bool RunMicrosoftBuild(string solutionFilePath, string mode)
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

			// Create the project object
			var project = new Project(engine);

			// Set the configuration/mode
			project.SetProperty("Configuration", mode);

			// Build the project
			success = project.Build();

			// Unregister all loggers to close the log file
			engine.UnregisterAllLoggers();

			if (success)
				Console.WriteLine("Build succeeded.");
			else
			{
				isError = true;
				Console.WriteLine(@"Build failed. View C:\temp\build.log for details");
			}

			return success;
		}

		public bool RunXBuild(string solutionFilePath, string mode)
		{
			var cmdName = "xbuild";

			var arguments = new string[]{
				"\"" + solutionFilePath + "\"",
				"/property:Configuration=" + mode
			};

			var cmd = StartProcess(
				cmdName,
				arguments
			);

			cmd.CommandProcess.WaitForExit();

		//	var output = cmd.CommandProcess.StandardOutput.ReadToEnd();

		//	var zeroErrorsNotFound = (output.IndexOf("0 Error(s)") == -1);

			var exitCodeSuccess = (cmd.CommandProcess.ExitCode == 0);

			if (
				exitCodeSuccess
			//    && zeroErrorsNotFound
			)

			{
            	Console.WriteLine("Build succeeded.");

				return true;
			}
			else
			{
				Error ("Build failed.");

				return false;
			}
		}
		
	}
}
