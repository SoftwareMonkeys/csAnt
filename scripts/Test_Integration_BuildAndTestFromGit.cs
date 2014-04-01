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
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_BuildAndTestFromGitScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_BuildAndTestFromGitScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from git clone...");
		Console.WriteLine("");
		
		// Clone the project to another directory
		Clone();

		// TODO: Check the use of FilesGrabber. It's used to grab libraries, but also grabs scripts. It might be better to specify just the libraries and not the scripts, so the test is more comprehensive
		new FilesGrabber(
			OriginalDirectory,
			CurrentDirectory
		).GrabOriginalScriptingFiles();

		if (!IsError)
		{
			// Build and test the cloned copy of the project
			BuildAndTestClonedCopy();
		}

		return !IsError;
	}

	public void Clone()
	{
		Console.WriteLine("Cloning to tmp directory...");

		GitClone(OriginalDirectory, CurrentDirectory);
	}

	public void BuildAndTestClonedCopy()
	{
		// Build and test
		ExecuteScript("CycleTests");
	}

}
