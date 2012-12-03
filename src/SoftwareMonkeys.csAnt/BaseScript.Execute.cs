using System;
using System.Diagnostics;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		public StartProcessCommand Execute(string command, params string[] arguments)
		{
			return Execute(command, String.Join(" ", arguments));
		}
		
		public StartProcessCommand Execute(string command, string arguments)
		{
			// Use the StartProcess function (this Execute function is just an alias)
			return StartProcess(command + " " + arguments);
		}
	}
}
