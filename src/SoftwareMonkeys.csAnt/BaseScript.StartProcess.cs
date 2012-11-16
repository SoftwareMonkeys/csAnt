using System;
using System.Diagnostics;

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
		public Process StartProcess(string command, string[] arguments)
		{
			return StartProcess(command, String.Join(" ", arguments));
		}
		
		/// <summary>
		/// Starts/executes a process in the current thread.
		/// </summary>
		/// <returns>
		/// The newly started process.
		/// </returns>
		/// <param name='command'></param>
		/// <param name='arguments'></param>
		public Process StartProcess(string command, string arguments)
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

			return process;
		}
	}
}

