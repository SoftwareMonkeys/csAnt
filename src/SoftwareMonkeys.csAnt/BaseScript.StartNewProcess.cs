using System;
using System.Diagnostics;

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

		/// <summary>
		/// Starts/executes a process in a new thread.
		/// </summary>
		/// <returns>
		/// The newly started process.
		/// </returns>
		/// <param name='command'></param>
		/// <param name='arguments'></param>
		public Process StartNewProcess(string command, string arguments)
		{
			Console.WriteLine("Executing command: " + command);
			Console.WriteLine("Arguments: " + arguments);

			// Create the process start info
			ProcessStartInfo info = new ProcessStartInfo(command, arguments);

			// Configure the process
			info.UseShellExecute = true;

			info.RedirectStandardOutput = false;
			info.RedirectStandardError = false;
			info.RedirectStandardInput = false;

			info.CreateNoWindow = false;

			info.ErrorDialog = false;

	        info.WindowStyle = ProcessWindowStyle.Normal;

			// Start the process
			Process process = Process.Start(info);

			return process;

		}
	}
}

