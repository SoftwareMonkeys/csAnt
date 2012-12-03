using System;
using SoftwareMonkeys.csAnt.Commands;
using SoftwareMonkeys.Jungle.Injection;

namespace SoftwareMonkeys.csAnt
{
	[ScriptCommand]
	public class HelloWorldCommand : BaseScriptCommand
	{
		public HelloWorldCommand(
			IScript script
		)
			: base(
				script
			)
		{
		}

		public override void Execute ()
		{
			Console.WriteLine ("");
			Console.WriteLine ("Hello World");
			Console.WriteLine ("");
		}
	}
}

