using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Commands
{
	public abstract class BaseScriptCommand : IScriptCommand, IDisposable
	{
		public IScript Script { get;set; }

		public object ReturnValue { get;set; }

		public BaseScriptCommand(
			IScript script
		)
		{
			Script = script;
            Console.SetOut ((TextWriter)Script.ConsoleWriter);
		}

		public abstract void Execute();

		public virtual void Dispose()
		{
		}
	}
}

