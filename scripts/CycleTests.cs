//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CycleTestsScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CycleTestsScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full release cycle.");
		Console.WriteLine("");

		ExecuteScript("CycleBuild");

		if (!IsError)
		{
			Console.WriteLine("Running the tests...");
			Console.WriteLine("");

			ExecuteScript("RunTests");
		}

		return !IsError;
	}
}
