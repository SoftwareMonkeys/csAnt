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
		Console.WriteLine("Starting a full cycle.");
		Console.WriteLine("");
		
		bool test = Arguments.ContainsAny("t", "test");
		
		ExecuteScript("IdentifyVersion");

		// Build the solutions
		ExecuteScript(
			"EnsureBuild"
		);

		if (!IsError)
		{
            if (test)
            {
			    Console.WriteLine("Testing and creating reports...");
			    Console.WriteLine("");

			    // Run tests
			    ExecuteScript(
				    "CycleTests"
			    );
		    }
		}

		if (!IsError)
		{
			Console.WriteLine("Creating and publish packages...");
			Console.WriteLine("");

			// Run release scripts
			ExecuteScript(
				"CyclePublish"
			);
		}
		
		return true;
	}
}
