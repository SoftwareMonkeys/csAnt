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
		public Process StartProcess(string command, params string[] arguments)
		{
            // TODO: Move ProcessStarter to a property
			return new ProcessStarter().Start(command, String.Join(" ", arguments));
		}
		
		/// <summary>
		/// Starts/executes a process in the current thread.
		/// </summary>
		/// <returns>
		/// The start process command.
		/// </returns>
		/// <param name='command'></param>
		/// <param name='arguments'></param>
		public Process StartProcess(string command)
		{
            return new ProcessStarter().Start(command, "");
		}
	}
}

