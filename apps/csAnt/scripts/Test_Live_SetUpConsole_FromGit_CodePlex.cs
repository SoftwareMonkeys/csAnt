//css_ref "SoftwareMonkeys.csAnt.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.Scripting.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll";
//css_ref "SoftwareMonkeys.csAnt.SourceControl.Git.dll";
//css_ref "SoftwareMonkeys.csAnt.SetUp.dll";

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting.Live;
using SoftwareMonkeys.csAnt.Tests.Helpers;
using SoftwareMonkeys.csAnt.SourceControl.Git;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.SetUp.Deploy.Launch;

class Test_SetUpFromGitScript : BaseLiveProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_SetUpFromGitScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		// TODO: Better organize this script

		Console.WriteLine("");
		Console.WriteLine("Test building solutions from CodePlex git clone...");
		Console.WriteLine("");

        var gitPath = "https://git01.codeplex.com/csant";

        // Clone from CodePlex
        new Gitter().Clone(gitPath, CurrentDirectory);
   
        // Run the setup script
        new SetUpConsoleLauncher().Launch(CurrentDirectory);

        // Test the hello world script to ensure setup worked
        new HelloWorldScriptLauncher().Launch();

		return !IsError;
	}

}
