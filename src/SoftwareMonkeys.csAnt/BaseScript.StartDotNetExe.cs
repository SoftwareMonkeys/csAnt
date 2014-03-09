using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public Process StartDotNetExe(string exeFile, params string[] arguments)
		{
            return new DotNetProcessStarter().Start(exeFile, arguments);
		}
	}
}

