//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

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
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full local cycle.");
		Console.WriteLine("");

		Console.WriteLine("Building...");
		Console.WriteLine("");

		// TODO: Check if needed. Shouldn't be needed because CycleTests calls it
		// Build the solutions
		ExecuteScript(
			"CycleBuild"
		);

		if (!IsError)
		{
			Console.WriteLine("Testing and creating reports	...");
			Console.WriteLine("");

			// Run tests
			ExecuteScript(
				"RunTests"
			);
		}

		if (!IsError)
		{
			Console.WriteLine("Creating release zip files locally...");
			Console.WriteLine("");

			// Run tests
			ExecuteScript(
				"Release"
			);
		}
		
		return true;
	}
}