//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;

class TestBuildFromStandardReleaseScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new TestBuildFromStandardReleaseScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from the release files...");
		Console.WriteLine("");

		Console.WriteLine("Building...");
		Console.WriteLine("");

		// Build and create release zips for the solution
		ExecuteScript("Release", "standard-release");

		if (!IsError)
		{
			var rlsDir = ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "rls"
				+ Path.DirectorySeparatorChar
				+ "standard-release";

			var latest = GetNewestFile(rlsDir);

			UnzipAndPrepare(latest);
		}

		return !IsError;
		
	}

	public void UnzipAndPrepare(string latestFile)
	{
		var tmpDir = GetTmpDir();

		Console.WriteLine("Tmp dir: " + tmpDir);

		Unzip(latestFile, tmpDir, "*");

		ProjectDirectory = tmpDir;

		// Run the prepare scripts
		PrepareProject(ProjectDirectory);

		// Delete the temporary directory
		Directory.Delete(tmpDir, true);
	}
}
