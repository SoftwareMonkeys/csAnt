//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SourceControl.Git.dll;

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

class Test_SetUpFromLocalScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_SetUpFromLocalScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Testing the setup from local script...");
		Console.WriteLine("");

        var testProjectDir = Path.GetDirectoryName(CurrentDirectory)
            + Path.DirectorySeparatorChar
            + "TestProject";

        var sourceDir = CurrentDirectory;

        Prepare(sourceDir, testProjectDir);

        // Run the setup from local script
        new SetUpFromLocalScriptLauncher().Launch(sourceDir, testProjectDir);

        // Test the hello world script to ensure setup worked
        new HelloWorldScriptLauncher().Launch();

		return !IsError;
	}

    public void Prepare(string sourceDir, string testProjectDir)
    {
        new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalFiles();

        ExecuteScript("EnsureBuild");

        EnsureDirectoryExists(testProjectDir);

        Relocate(testProjectDir);

        new SetUpFromLocalScriptRetriever().Retrieve(sourceDir, testProjectDir);

    }

}
