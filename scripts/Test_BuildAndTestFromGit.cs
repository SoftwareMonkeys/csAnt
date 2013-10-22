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

class Test_BuildAndTestFromSourceReleaseScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_BuildAndTestFromSourceReleaseScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from git clone...");
		Console.WriteLine("");

		// Clone the project to another directory
		var tmpDir = CloneToTmpDirectory();

		// Prepare the project (eg. download libs)
		Prepare(tmpDir);

		if (!IsError)
		{
			// Build and test the cloned copy of the project
			BuildAndTestClonedCopy(tmpDir);
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

		return tmpDirectory;
	}

	public void Prepare(string tmpDir)
	{
		PrepareProject(tmpDir);
	}

	public void BuildAndTestClonedCopy(string tmpDir)
	{
		CurrentDirectory = tmpDir;

		// Build and test
		ExecuteScript("CycleTests");

		CopyTestResults(tmpDir, ProjectDirectory);
	}

}
