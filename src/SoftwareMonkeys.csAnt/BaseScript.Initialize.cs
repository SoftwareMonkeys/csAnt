using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void Initialize(string scriptName)
		{
			// TODO: Inject the ConsoleWriter via constructor/creator
			if (Console == null)
				Console = new ConsoleWriter("logs", scriptName); // Move term "logs" to somewhere more easily configured
		}
		
		public virtual void Initialize(string scriptName, ConsoleWriter consoleWriter)
		{
			Console = consoleWriter;
		}
	}
}

