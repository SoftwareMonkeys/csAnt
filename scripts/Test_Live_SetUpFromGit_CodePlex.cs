//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll;
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

class Test_SetUpFromGitScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_SetUpFromGitScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		// TODO: Better organize this script

		Console.WriteLine("");
		Console.WriteLine("Test building solutions from git clone...");
		Console.WriteLine("");

		new FilesGrabber(
                    OriginalDirectory,
                    CurrentDirectory
                ).GrabOriginalFiles();

		// Clone the project to another directory
		var dummyProjectDir = CloneToTmpDirectory();

		SetUpClonedCopy(dummyProjectDir);

        StartHelloWorld();

		return !IsError;
	}

    public void StartHelloWorld()
    {
		if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld");
    }

	public void EnsurePackages()
	{
        var testDir = CurrentDirectory;

		// Relocate back to the original directory to ensure that the packages have been created
		Relocate(OriginalDirectory);

        ExecuteScript("EnsurePackage");

		// Relocated back to the test directory
		Relocate(testDir);
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

		Relocate(projectDirectory);

		CreateNodes();

		return projectDirectory;
	}

	public void SetUpClonedCopy(string dummyProjectDir)
	{
		if (IsLinux)
			StartProcess("sh csAnt-setup.sh");
		else
			throw new NotImplementedException("Windows support hasn't yet been implemented");
	}

}
