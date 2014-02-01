//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SetUpFromLocal.exe;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.SetUpFromLocalConsole;

class Test_BuildFromGitScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_BuildFromGitScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from git clone...");
		Console.WriteLine("");

                var testDir = CurrentDirectory;
		
		new FilesGrabber(
                    OriginalDirectory,
                    CurrentDirectory
                ).GrabOriginalFiles();

                ExecuteScript("CycleRelease", "project-release");

		// Clone the project to another directory
		var dummyProjectDir = CloneToTmpDirectory();

		Relocate(dummyProjectDir);

		CreateNodes();

		var fileList = ToAbsolute(OriginalDirectory, "install/csAnt-installer.txt");

		var installer = new LocalInstaller();
                installer.Install (
                    OriginalDirectory,
                    dummyProjectDir,
                    fileList,
                    true
                );

		if (!IsError)
		{
			// Build and test the cloned copy of the project
			BuildClonedCopy(dummyProjectDir);
		}

		return !IsError;
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

		GitClone(OriginalDirectory, projectDirectory);

		return projectDirectory;
	}

	public void BuildClonedCopy(string dummyProjectDir)
	{
		CurrentDirectory = dummyProjectDir;

		// Build and test
		ExecuteScript("CycleBuild");
	}

}
