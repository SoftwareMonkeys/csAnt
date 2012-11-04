//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class DeployLocalScript : BaseScript
{
	public static void Main(string[] args)
	{
		new DeployLocalScript().Start(new Arguments(args));
	}
	
	public void Start(Arguments arguments)
	{
		// Arguments expected:
		// "--to:[destination]"

		Console.WriteLine("");
		Console.WriteLine("Deploying essential files to a local destination...");
		Console.WriteLine("");

		var destination = Path.GetFullPath(arguments["to"]);

		Console.WriteLine("Destination:");
		Console.WriteLine(destination);

		var releaseDir = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "rls"
			+ Path.DirectorySeparatorChar
			+ "release";

		var releaseFile = GetNewestFile(releaseDir);

		Console.WriteLine("Release file:");
		Console.WriteLine(releaseFile);

		Unzip(releaseFile, destination);

		// Move the sub directory into the specified destination
		MoveDirectory(
			GetNewestFolder(destination),
			destination
		);

		var prepareScriptFile = "prepare.sh";

		Environment.CurrentDirectory = destination;

		// Execute the prepare script
		StartProcess(
			"sh",
			prepareScriptFile
		);
	}
}
