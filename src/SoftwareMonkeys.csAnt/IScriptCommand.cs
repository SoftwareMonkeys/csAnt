using System;

namespace SoftwareMonkeys.csAnt
{
	public interface IScriptCommand
	{
		object ReturnValue { get;set; }

		void Execute();
	}
}

