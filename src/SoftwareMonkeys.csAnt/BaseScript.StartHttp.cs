using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		public Process StartHttp(string physicalPath, string host, int port)
		{
			// TODO: Make the xsp4 server path configurable

			// Windows
			// TODO: Add support for starting HTTP server (likely XSP4) under windows

			// Linux
			var xspExe = "xsp4";
		
			Console.WriteLine("XSP exe file: " + xspExe);

			List<string> parameters = new List<string>();

			// Add all the parameters
			parameters.Add ("--port " + port);
			parameters.Add ("--root '" + physicalPath + "'");
			parameters.Add ("--address " + host);

			// If the script is running in verbose mode then make XSP4 run in verbose mode too
			if (IsVerbose)
				parameters.Add ("--verbose");

			// Launch the XSP4 (HTTP server) process
			var process = StartNewProcess(
				xspExe,
				parameters.ToArray()
			);

			Console.WriteLine ("HTTP Server Started!...");

			return process;
		}
	}
}
