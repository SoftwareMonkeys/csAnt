using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void ExecuteCommand(IScriptCommand command)
		{
			command.Execute();
		}
	}
}

