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
			info.RedirectStandardInput = true;
			info.CreateNoWindow = false;

			info.ErrorDialog = false;

			// Start the process
			Process process = Process.Start(info);

			// Write the process output to the console while it runs
			while (!process.HasExited)
			{
				string output = String.Empty;
				
				if (!process.StandardOutput.EndOfStream)
				{
					output = process.StandardOutput.ReadLine();
					
					if (output != null)
						Console.WriteLine(output);
				}
			}
				
			// Write the process errors to the console
			if (!process.StandardError.EndOfStream)
			{
				string error = String.Empty;

				error = process.StandardError.ReadLine();
				
				if (error != null)
					Console.WriteLine(error);
			}

			return process;
		}
	}
}

