using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void InitializeConsoleWriter(string scriptName, ConsoleWriter consoleWriter)
		{
			Console = consoleWriter;

			Console.Script = this;
		}
	}
}

