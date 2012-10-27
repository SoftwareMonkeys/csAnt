using System;
using System.Diagnostics;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		public void Execute(string command, params string[] arguments)
		{
			Execute(command, String.Join(" ", arguments));
		}
		
		public void Execute(string command, string arguments)
		{
			// Use the StartProcess function (this Execute function is just an alias)
			StartProcess(command, arguments);
		}
	}
}
