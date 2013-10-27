using System;
using SoftwareMonkeys.csAnt.Commands;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SoftwareMonkeys.csAnt
{
	public class StartNewProcessCommand : BaseScriptCommand
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

		public Process SubProcess { get;set; }


		public StartNewProcessCommand (
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

		public StartNewProcessCommand (
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

			ThreadPool.QueueUserWorkItem(
				delegate
				{
					SubProcess = Process.Start( command );
				}
			);
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

		public override void Dispose ()
		{
			if (SubProcess != null) {
				SubProcess.Kill ();
				SubProcess = null;
			}
		}
	}
}

