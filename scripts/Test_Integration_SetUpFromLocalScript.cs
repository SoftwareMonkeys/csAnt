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
		Console.WriteLine("Testing the setup from local script...");
		Console.WriteLine("");

        ExecuteScriptAt(OriginalDirectory, "Repack");
        ExecuteScriptAt(OriginalDirectory, "CopyBinToLib");
        ExecuteScriptAt(OriginalDirectory, "CopyBinToRoot");

        new SetUpFromLocalScriptRetriever().Retrieve(OriginalDirectory, CurrentDirectory);

        // Run the setup from local script
        new SetUpFromLocalScriptLauncher().Launch(OriginalDirectory, CurrentDirectory);

        // Test the hello world script to ensure setup worked
        new HelloWorldScriptLauncher().Launch();

		return !IsError;
	}

}
