using System;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt
{
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

			Script.AddSummary("Printed out the words 'Hello world'.");
		}
	}
}

