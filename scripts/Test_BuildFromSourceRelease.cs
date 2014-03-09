//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
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
		
		var testDir = CurrentDirectory;

		Relocate(OriginalDirectory);

		ExecuteScript("EnsureRelease", "src");

		Relocate(testDir);

		if (!IsError)
		{
			DeployAndBuild();
		}
		
		return !IsError;
	}

	public void DeployAndBuild()
	{
                var tmpDir = GetTmpDir();

		var releaseZipFile = GetNewestFile(
			OriginalDirectory
			+ Path.DirectorySeparatorChar
			+ "rls"
			+ Path.DirectorySeparatorChar
			+ "src"
		);

		Unzip(releaseZipFile, tmpDir, "*");			

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
