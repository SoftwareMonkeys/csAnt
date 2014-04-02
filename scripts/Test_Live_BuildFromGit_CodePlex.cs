//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_BuildFromGitScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_BuildFromGitScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from CodePlex git clone...");
		Console.WriteLine("");

		var dummyProjectDir = PrepareTest();

		if (!IsError)
		{
			// Build and test the cloned copy of the project
			BuildClonedCopy(dummyProjectDir);
		}

		return !IsError;
	}

	public string PrepareTest()
	{
        var testDir = CurrentDirectory;
		
		new FilesGrabber(
                    OriginalDirectory,
                    CurrentDirectory
        ).GrabOriginalScriptingFiles();

		// Clone the project to another directory
		var dummyProjectDir = CloneToTmpDirectory();

		// Relocate to the dummy project directory
		Relocate(dummyProjectDir);

		// Create the required nodes
		CreateNodes();

		// Grab the library files (so there's no need to run setup)
		new FilesGrabber(
                    testDir,
                    dummyProjectDir
                ).GrabOriginalFiles(
			"lib/**"
		);

		return dummyProjectDir;
	}

	public string CloneToTmpDirectory()
	{
		Console.WriteLine("Cloning to tmp directory...");

		var projectDirectory = Path.GetDirectoryName(CurrentDirectory)
                    + Path.DirectorySeparatorChar
                    + "TestProject";

		Console.WriteLine("Tmp directory:");
		Console.WriteLine(projectDirectory);

		Directory.CreateDirectory(projectDirectory);

		GitClone("https://git01.codeplex.com/csant", projectDirectory);

		return projectDirectory;
	}

	public void BuildClonedCopy(string dummyProjectDir)
	{
		CurrentDirectory = dummyProjectDir;

		// Build and test
		ExecuteScript("CycleBuild");
	}

}
