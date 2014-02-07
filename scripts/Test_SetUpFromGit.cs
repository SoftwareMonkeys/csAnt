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

		EnsureReleases();

		new FilesGrabber(
                    OriginalDirectory,
                    CurrentDirectory
                ).GrabOriginalFiles();

		// Clone the project to another directory
		var dummyProjectDir = CloneToTmpDirectory();

		var fileList = ToAbsolute(OriginalDirectory, "install/csAnt-installer.txt");

		var installer = new LocalInstaller();

		// Install csAnt to the dummy project directory
                installer.Install (
                    OriginalDirectory,
                    dummyProjectDir,
                    fileList,
                    true
                );

		/*if (!IsError)
		{
			// Build and test the cloned copy of the project
			SetUpClonedCopy(dummyProjectDir);
		}*/

		return !IsError;
	}

	public void EnsureReleases()
	{
                var testDir = CurrentDirectory;

		// Relocate back to the original directory to ensure that the releases have been created
		Relocate(OriginalDirectory);

                ExecuteScript("EnsureRelease");

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

		GitClone(OriginalDirectory, projectDirectory);

		Relocate(projectDirectory);

		CreateNodes();

		return projectDirectory;
	}

	public void SetUpClonedCopy(string dummyProjectDir)
	{
		CurrentDirectory = dummyProjectDir;

		// Build and test
		ExecuteScript("CycleBuild");
	}

}
