//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;

class Test_BuildAndTestFromSourceReleaseScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_BuildAndTestFromSourceReleaseScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from the release files...");
		Console.WriteLine("");

		ExecuteScript("CycleBuild");

		if (!IsError)
			ExecuteScript("Release", "src");

		if (!IsError)
		{
			var rlsDir = ProjectDirectory
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

		var tmpDir = GetTmpDir();

		var subDir = Path.GetFileNameWithoutExtension(latestFile);

		Unzip(latestFile, tmpDir, subDir);

		Console.WriteLine("");
		Console.WriteLine("Tmp dir:");
		Console.WriteLine(" " + tmpDir);
		Console.WriteLine("");

		var originalProjectDirectory = ProjectDirectory;

		Console.WriteLine("Project directory:");
		Console.WriteLine(originalProjectDirectory);

		// Move to the tmp directory
		ProjectDirectory = tmpDir;

		Console.WriteLine("");
		Console.WriteLine("Preparing...");
		Console.WriteLine("");

		PrepareProject(ProjectDirectory);

		if (!IsError)
		{
			Console.WriteLine("");
			Console.WriteLine("Testing build...");
			Console.WriteLine("");

			BuildAllSolutions(
				ProjectDirectory
				+ "/src"
			);
		}

		// Suspend most of the test scripts so they dont need to execute (speeds up the test)
		SuspendScripts();

		ExecuteScript("RunTests");

		Utilities.CopyTestResults(ProjectDirectory, originalProjectDirectory);

		//Directory.Delete(ProjectDirectory, true);

		// Move back to the original project directory
		ProjectDirectory = originalProjectDirectory;
	}

	public void SuspendScripts()
	{
		var scriptsDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "scripts";

		foreach (var file in Directory.GetFiles(scriptsDir))
		{
			var fileName = Path.GetFileNameWithoutExtension(file);

			if (fileName.StartsWith("Test_")
				&& fileName != "Test_HelloWorld")
			{
				var newName = scriptsDir
					+ Path.DirectorySeparatorChar
					+ Path.GetFileNameWithoutExtension(file)
					+ "._cs";

				File.Move(file, newName);
			}
		}
	}
}
