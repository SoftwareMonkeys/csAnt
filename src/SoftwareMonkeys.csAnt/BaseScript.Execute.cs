using System;
using System.Diagnostics;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		public Process Execute(string command, params string[] arguments)
		{
			return Execute(command, String.Join(" ", arguments));
		}
		
		public Process Execute(string command, string arguments)
		{
			// Use the StartProcess function (this Execute function is just an alias)
			return StartProcess(command + " " + arguments);
		}
        
        public void Execute(BaseScriptCommand command)
        {
            ExecuteCommand(command);
        }
	}
}
