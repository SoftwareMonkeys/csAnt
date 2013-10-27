using System;

namespace SoftwareMonkeys.csAnt.Commands
{
	public abstract class BaseScriptCommand : IScriptCommand, IDisposable
	{
		public IScript Script { get;set; }

		public object ReturnValue { get;set; }

		public ConsoleWriter Console { get; set; }

		public BaseScriptCommand(
			IScript script
		)
		{
			Script = script;
			Console = script.Console;
		}

		public abstract void Execute();

		public virtual void Dispose()
		{
		}
	}
}

