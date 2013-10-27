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

		// Build the whole project first
		ExecuteScript("CycleBuild");


		// Git clone the project to another directory
		var tmpDir = CloneToTmpDirectory();

		// Prepare the project (eg. download libs)
		InitializeProject(tmpDir);

		// Move to the cloned directory
		CurrentDirectory = tmpDir;

		// Build the cloned source code
		ExecuteScript("CycleBuild");

		Console.WriteLine("");
		Console.WriteLine("Creating release zip files...");
		Console.WriteLine("");

		// Create the release
		ExecuteScript(
			"Release",
			new string[]{
				"-mode:Release"
			}
		);

		if (!IsError)
		{
			Console.WriteLine("Uploading the release zip file to GoogleCode...");
			Console.WriteLine("");

			// Upload to GoogleCode
			ExecuteScript("GoogleCodeRelease");
		}

		return !IsError;
	}

	public string CloneToTmpDirectory()
	{
		Console.WriteLine("Cloning to tmp directory...");

		var tmpDirectory = GetTmpDir();

		Console.WriteLine("Tmp directory:");
		Console.WriteLine(tmpDirectory);

		Directory.CreateDirectory(tmpDirectory);

		GitClone(ProjectDirectory, tmpDirectory);

		AddSummary("Cloned project to: " + tmpDirectory);

		return tmpDirectory;
	}
}
