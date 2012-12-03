//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class TestBuildFromSourceReleaseScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new TestBuildFromSourceReleaseScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from the release files...");
		Console.WriteLine("");

		ExecuteScript("CycleBuild");

		ExecuteScript("Release");

		if (!IsError)
		{
			var rlsDir = ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "rls"
				+ Path.DirectorySeparatorChar
				+ "src";

			var latest = GetNewestFile(rlsDir);

			UnzipAndBuild(latest);
		}
		
		return !IsError;
	}

	public void UnzipAndBuild(string latestFile)
	{

		Console.WriteLine("");
		Console.WriteLine("Unzipping...");
		Console.WriteLine("");

		var tmpDir = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "_tmp"
			+ Path.DirectorySeparatorChar
			+ "testing"
			+ Path.DirectorySeparatorChar
			+ Guid.NewGuid().ToString()
			+ Path.DirectorySeparatorChar
			+ ProjectName;

		Unzip(latestFile, tmpDir);

		Console.WriteLine("");
		Console.WriteLine("Tmp dir:");
		Console.WriteLine(" " + tmpDir);
		Console.WriteLine("");

		var subDir = GetNewestFolder(tmpDir); 

		// Move from the sub directory to the intended directory
		MoveDirectory(
			subDir,
			tmpDir
		);

		ProjectDirectory = tmpDir;

		Console.WriteLine("");
		Console.WriteLine("Preparing...");
		Console.WriteLine("");

		PrepareProject(ProjectDirectory);

		if (!IsError)
		{
			Console.WriteLine("");
			Console.WriteLine("Testing build...");
			Console.WriteLine("");

			BuildAllSolutions(
				ProjectDirectory
				+ "/src"
			);
		}

		Directory.Delete(ProjectDirectory, true);
	}
}
