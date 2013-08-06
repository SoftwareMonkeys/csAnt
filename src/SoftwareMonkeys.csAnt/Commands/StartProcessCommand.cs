using System;
using SoftwareMonkeys.csAnt.Commands;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public class StartProcessCommand : BaseScriptCommand
	{
		public Process CommandProcess { get;set; }

		public string Command { get;set; }

		public string[] Arguments { get;set; }

		public string CommandWithArguments
		{
			get
			{
				return Command + " " + String.Join(" ", Arguments);
			}
			set
			{
				var parts = new List<string>();
				parts.AddRange(value.Split(' '));

				if (parts.Count > 0)
					Command = parts[0];

				if (parts.Count > 1)
				{
					parts.RemoveAt(0);
					Arguments = parts.ToArray ();
				}
			}
		}


		public StartProcessCommand (
			IScript script,
			string command,
			string[] arguments
		)
			: this(
				script,
				command + " " + String.Join(" ", arguments)
			)
		{
			Command = command;

			Arguments = arguments;
		}

		public StartProcessCommand (
			IScript script,
			string commandWithArguments
		)
			: base(script)
		{
			CommandWithArguments = commandWithArguments;
		}

		public override void Execute()
		{
			StartProcess(
				Command,
				Arguments
			);
		}

		/// <summary>
		/// Starts/executes a process in the current thread.
		/// </summary>
		/// <param name='command'></param>
		/// <param name='arguments'></param>
		public void StartProcess(string command, string[] arguments)
		{
			arguments = FixArguments(arguments);

			 StartProcess(command, String.Join(" ", arguments));
		}
		
		/// <summary>
		/// Starts/executes a process in the current thread.
		/// </summary>
		/// <param name='command'></param>
		/// <param name='arguments'></param>
		public void StartProcess(string command, string arguments)
		{
			Console.WriteLine("");
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

			CommandProcess = process;

			process.StartInfo = info;

			process.EnableRaisingEvents = true;

			// Output the data to the console
			process.OutputDataReceived += new DataReceivedEventHandler
			(
			    delegate(object sender, DataReceivedEventArgs e)
			    {
			        Console.WriteLine(e.Data);
			    }
			);
			
			// Output the errors to the console
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

			Console.WriteLine("");
			Console.WriteLine("--------------------------------------------------");
			Console.WriteLine("");
			
		}

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


	}
}

