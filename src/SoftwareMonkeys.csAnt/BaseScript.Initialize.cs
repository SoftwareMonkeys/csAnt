using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void Initialize(string scriptName)
		{
			ScriptName = scriptName;
			
			TimeStamp = GetTimeStamp();

			InitializeConsoleWriter(scriptName, new ConsoleWriter("logs", scriptName));
		}
		
		public virtual void Initialize(string scriptName, ConsoleWriter consoleWriter)
		{
			ScriptName = scriptName;
			
			TimeStamp = GetTimeStamp();

			InitializeConsoleWriter(scriptName, consoleWriter);
		}
	}
}

