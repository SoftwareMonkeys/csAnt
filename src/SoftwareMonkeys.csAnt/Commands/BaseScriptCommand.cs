using System;

namespace SoftwareMonkeys.csAnt.Commands
{
	[ScriptCommand]
	public abstract class BaseScriptCommand : IScriptCommand
	{
		public IScript Script { get;set; }

		public object ReturnValue { get;set; }

		public BaseScriptCommand(
			IScript script
		)
		{
			Script = script;
		}

		public abstract void Execute();
	}
}

