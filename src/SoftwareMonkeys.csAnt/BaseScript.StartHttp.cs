using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		public void StartHttp (string physicalPath, string host, int port)
		{
			StartHttp(
				physicalPath,
				host,
				port,
				true
			);
		}

		public void StartHttp(string physicalPath, string host, int port, bool autoKill)
		{
			// TODO: Make the xsp4 server path configurable

			// Windows
			// TODO: Add support for starting HTTP server (likely XSP4) under windows

			// Linux
			var xspExe = "xsp4";

			List<string> parameters = new List<string>();

			// Add all the parameters
			parameters.Add ("--port " + port);
			parameters.Add ("--root '" + physicalPath + "'");

			if (host != "0.0.0.0")
				parameters.Add ("--address " + host);

			// If the script is running in verbose mode then make XSP run in verbose mode too
			if (IsVerbose)
				parameters.Add ("--verbose");

			// Launch the XSP4 (HTTP server) process
			StartNewProcess(
				xspExe,
				parameters.ToArray()
			);

			// TODO: Check if needed
			// If autoKill is true, add the process to the SubProcesses list, so it can be killed on dispose
			//if (autoKill)
			//	SubProcesses.Add(process);

			Console.WriteLine ("HTTP Server Started!...");
		}
	}
}
