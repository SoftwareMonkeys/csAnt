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
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_BuildFromSourceReleaseScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_BuildFromSourceReleaseScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from the release files...");
		Console.WriteLine("");
		
		FilesGrabber.GrabOriginalFiles();

		ExecuteScript("CycleRelease", "src");

		if (!IsError)
		{
			DeployAndBuild();
		}
		
		return !IsError;
	}

	public void DeployAndBuild()
	{
                var tmpDir = GetTmpDir();
                
                InstallTo("csAnt-project", tmpDir);

		Console.WriteLine("");
		Console.WriteLine("Tmp dir:");
		Console.WriteLine(" " + tmpDir);
		Console.WriteLine("");

		Relocate(tmpDir);

		if (!IsError)
		{
			Console.WriteLine("");
			Console.WriteLine("Building...");
			Console.WriteLine("");

			ExecuteScript("CycleBuild");
		}

		// Move back to the original project directory
		Relocate(OriginalDirectory);
	}
}
