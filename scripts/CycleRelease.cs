//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CycleReleaseScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CycleReleaseScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full release cycle.");
		Console.WriteLine("");

		ExecuteScript("CycleBuild");

		Console.WriteLine("Creating release zip files...");
		Console.WriteLine("");

		// Create the release
		ExecuteScript(
			"Release",
			new string[]{
				"-mode:Release"
			}
		);

		return !IsError;
	}
}
