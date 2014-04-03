using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CycleScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CycleScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full local cycle.");
		Console.WriteLine("");

		Console.WriteLine("Building...");
		Console.WriteLine("");

		// Build the solutions
		ExecuteScript(
			"EnsureBuild"
		);

        // TODO: Remove if not needed. Tests take a long time and should be done separately.
		/*if (!IsError)
		{
			Console.WriteLine("Testing and creating reports...");
			Console.WriteLine("");

			// Run tests
			ExecuteScript(
				"RunTests"
			);
		}*/

		if (!IsError)
		{
			Console.WriteLine("Creating package files locally...");
			Console.WriteLine("");

			// Run release scripts
			ExecuteScript(
				"CyclePackage"
			);
		}
		
		return true;
	}
}
