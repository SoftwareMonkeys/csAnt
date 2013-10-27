using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		/// <summary>
		/// Starts/executes a process in a new thread.
		/// </summary>
		/// <returns>
		/// The newly started process.
		/// </returns>
		/// <param name='command'></param>
		/// <param name='arguments'></param>
		public Process StartNewProcess(string command, params string[] arguments)
		{
			return StartNewProcess(command, String.Join (" ", arguments));
		}

		public Process StartNewProcess (string command, string arguments)
		{
			return StartNewProcess(command, arguments, true);
		}

		/// <summary>
		/// Starts/executes a process in a new thread.
		/// </summary>
		/// <returns>
		/// The newly started process.
		/// </returns>
		/// <param name='command'></param>
		/// <param name='arguments'></param>
		public Process StartNewProcess(string command, string arguments, bool autoKill)
		{
			Console.WriteLine("");
			Console.WriteLine("Starting new process: " + command);
			Console.WriteLine("Arguments:");
			Console.WriteLine(arguments);
			Console.WriteLine("");

			// TODO: Implement autokill parameter or remove it
			var cmd = new StartNewProcessCommand(this, command, arguments.Split(' '));
	
			cmd.Execute();

			return (Process)cmd.ReturnValue;
		}
	}
}

