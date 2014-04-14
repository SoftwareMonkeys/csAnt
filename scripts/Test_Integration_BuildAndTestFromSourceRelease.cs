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
		Console.WriteLine("Building and testing source release...");
		Console.WriteLine("");
		
		var testDir = CurrentDirectory;

		Relocate(OriginalDirectory);

		ExecuteScript("EnsureRelease", "src");

		Relocate(testDir);

		if (!IsError)
		{
			var rlsDir = OriginalDirectory
				+ Path.DirectorySeparatorChar
				+ "rls"
				+ Path.DirectorySeparatorChar
				+ "src";

			var latest = GetNewestFile(rlsDir);

			UnzipAndBuild(latest);
		}
		
		return !IsError;
	}

	public void UnzipAndBuild(string latestFile)
	{

		Console.WriteLine("");
		Console.WriteLine("Unzipping...");
		Console.WriteLine("");

		Unzip(latestFile, CurrentDirectory, "*");

		// Refresh the nodes so it picks up on those that were unzipped
		Nodes.Refresh();

		if (!IsError)
		{
			Console.WriteLine("");
			Console.WriteLine("Building and testing...");
			Console.WriteLine("");

			ExecuteScript("CycleTests");
		}
	}
}
