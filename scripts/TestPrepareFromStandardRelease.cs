//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class TestBuildFromStandardReleaseScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new TestBuildFromStandardReleaseScript().Start();
	}
	
	public void Start()
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from the release files...");
		Console.WriteLine("");

		Console.WriteLine("Building...");
		Console.WriteLine("");

		// Build and create release zips for the solution
		ExecuteScript("Release");

		if (!IsError)
		{
			var rlsDir = ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "rls"
				+ Path.DirectorySeparatorChar
				+ "StandardRelease";

			var latest = GetNewestFile(rlsDir);

			UnzipAndPrepare(latest);
		}
		
	}

	public void UnzipAndPrepare(string latestFile)
	{
		var tmpDir = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "_tmp"
			+ Path.DirectorySeparatorChar
			+ Guid.NewGuid().ToString();

		Unzip(latestFile, tmpDir);

		var prepareShortcut = GetNewestFolder(tmpDir)
			+ Path.DirectorySeparatorChar
			+ "launch-prepare.sh";

		Console.WriteLine("Prepare script shortcut:");
		Console.WriteLine(prepareShortcut);

		if (File.Exists(prepareShortcut))
		{
			StartProcess(
				"bash",
				"\"" + prepareShortcut + "\""
			);
		}
		else
			Console.WriteLine("Can't find 'launch-prepare.sh' file.");
	}
}
