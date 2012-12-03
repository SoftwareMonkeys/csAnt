using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		/// <summary>
		/// Starts/executes a process in the current thread.
		/// </summary>
		/// <returns>
		/// The newly started process.
		/// </returns>
		/// <param name='command'></param>
		/// <param name='arguments'></param>
		public StartProcessCommand StartProcess(string command, params string[] arguments)
		{
			return StartProcess(command + " " + String.Join(" ", arguments));
		}
		
		/// <summary>
		/// Starts/executes a process in the current thread.
		/// </summary>
		/// <returns>
		/// The start process command.
		/// </returns>
		/// <param name='command'></param>
		/// <param name='arguments'></param>
		public StartProcessCommand StartProcess(string command)
		{
			var cmd = Injection.Retriever.Get<StartProcessCommand>();

			cmd.CommandWithArguments = command;

			ExecuteCommand(cmd);

			return cmd;

			// TODO: Remove if not needed
		/*	Console.WriteLine("");
			Console.WriteLine("--------------------------------------------------");
			Console.WriteLine("");
			Console.WriteLine("Starting process: " + command);
			Console.WriteLine("Arguments:");
			Console.WriteLine(arguments);
			Console.WriteLine("");

			// Create the process start information
			ProcessStartInfo info = new ProcessStartInfo(
				command,
				arguments
			);

			// Configure the process
			info.UseShellExecute = false;
			info.RedirectStandardOutput = true;
			info.RedirectStandardError = true;
			info.CreateNoWindow = true;

			info.ErrorDialog = false;

			// Start the process
			Process process = new Process();

			process.StartInfo = info;

			process.EnableRaisingEvents = true;

			process.OutputDataReceived += new DataReceivedEventHandler
			(
			    delegate(object sender, DataReceivedEventArgs e)
			    {
			        Console.WriteLine(e.Data);
			    }
			);

			process.ErrorDataReceived += new DataReceivedEventHandler
			(
			    delegate(object sender, DataReceivedEventArgs e)
			    {
			        Console.WriteLine(e.Data);
			    }
			);

			process.Start();

			process.BeginOutputReadLine();

			process.WaitForExit();

			return process;*/
		}
		/*
		public string[] FixArguments(string[] arguments)
		{
			List<string> argsList = new List<string>(arguments);

			for (int i = 0; i < argsList.Count; i++)
			{
				if (
					argsList[0].IndexOf(" ") > -1
				    && argsList[0].IndexOf("\"") != 0
				)
					argsList[0] = @"""" + argsList[0] + @"""";
			}

			return argsList.ToArray();
		}
*/

	}
}

