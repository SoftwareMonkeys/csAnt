using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public StartProcessCommand StartDotNetExe(string exeFile, string[] arguments)
		{
			string cmd = exeFile;
			
			List<string> argsList = new List<string>();

			if (IsMono)
			{
				cmd = "mono";
				argsList.Add(exeFile);
			}

			argsList.AddRange(arguments);

			argsList.Add("--verbose");

			return StartProcess(
				cmd,
				argsList.ToArray()
			);
		}
	}
}

