//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class DeployLocalScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new DeployLocalScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Deploying essential files to a local destination...");
		Console.WriteLine("");

		var destination = GetDestination(args);

		Console.WriteLine("Destination:");
		Console.WriteLine(destination);

		// Run the CycleRelease script
		ExecuteScript("CycleRelease");

		// TODO: Make it possible to specify either ProjectRelease or StandardRelease, depending on how
		// csAnt is to be used
		var releaseDir = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "rls"
			+ Path.DirectorySeparatorChar
			+ "project-release";

		var releaseFile = GetNewestFile(releaseDir);

		Console.WriteLine("Release file:");
		Console.WriteLine(releaseFile);

		// Unzip the release
		Unzip(releaseFile, destination);

		// Move the sub directory into the destination
		MoveDirectory(
			GetNewestFolder(destination),
			destination
		);

		// Run the prepare script in the new location
		PrepareProject(destination);

		return !IsError;
	}

	public string GetDestination(string[] args)
	{
		return Path.GetFullPath(args[0]);
	}
}
