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
			var cmd = new StartProcessCommand(
				this,
				command
			);

			ExecuteCommand(cmd);

			return cmd;
		}
	}
}

