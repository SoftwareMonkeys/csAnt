//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_BuildAndTestFromSourceReleaseScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_BuildAndTestFromSourceReleaseScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from git clone...");
		Console.WriteLine("");
		
		FilesGrabber.GrabOriginalFiles();

		// Clone the project to another directory
		var tmpDir = CloneToTmpDirectory();

		// Prepare the project
		InstallTo("csAnt", tmpDir);

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

		GitClone(OriginalDirectory, tmpDirectory);

		return tmpDirectory;
	}

	public void BuildAndTestClonedCopy(string tmpDir)
	{
		CurrentDirectory = tmpDir;

		// Build and test
		ExecuteScript("CycleTests");
	}

}
