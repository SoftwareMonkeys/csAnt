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

