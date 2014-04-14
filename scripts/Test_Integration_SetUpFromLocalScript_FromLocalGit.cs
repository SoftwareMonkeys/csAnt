//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SourceControl.Git.dll;
//css_ref ../lib/NUnit.2.6.0.12051/lib/nunit.framework.dll

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.Tests.Helpers;
using SoftwareMonkeys.csAnt.SourceControl.Git;
using SoftwareMonkeys.csAnt.SetUp;
using NUnit.Framework;

[TestFixture]
class Test_SetUpFromLocalScript_Git : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_SetUpFromLocalScript_Git().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		// TODO: Better organize this script

		Console.WriteLine("");
		Console.WriteLine("Test building solutions from git clone...");
		Console.WriteLine("");

        // Clone from original directory
        new Gitter().Clone(OriginalDirectory, CurrentDirectory);

        // Run the setup from local script
        new SetUpFromLocalScriptLauncher().Launch(OriginalDirectory, CurrentDirectory);

        // Test the hello world script to ensure setup worked
        new HelloWorldScriptLauncher().Launch();

		return !IsError;
	}

// TODO: Remove if not needed
	/*public void EnsurePackages()
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

		GitClone(OriginalDirectory, projectDirectory);

		Relocate(projectDirectory);

		CreateNodes();

		return projectDirectory;
	}

	public void SetUpClonedCopy(string dummyProjectDir)
	{
		if (IsLinux)
			StartProcess("sh csAnt-setupfromlocal.sh");
		else
			throw new NotImplementedException("Windows support hasn't yet been implemented");
	}*/

}
