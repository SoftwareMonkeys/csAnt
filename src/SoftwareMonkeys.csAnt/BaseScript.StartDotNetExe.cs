using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public Process StartDotNetExe(string exeFile, params string[] arguments)
		{
			string cmd = exeFile;
			
			List<string> argsList = new List<string>();

			if (IsMono)
			{
				cmd = "mono";

                // If the script is in debug mode then use --debug when executing 'mono [program.exe]'
                if (IsDebug)
                    argsList.Add ("--debug");
				argsList.Add(exeFile);
			}

			argsList.AddRange(arguments);

			return StartProcess(
				cmd,
				argsList.ToArray()
			);
		}
	}
}

