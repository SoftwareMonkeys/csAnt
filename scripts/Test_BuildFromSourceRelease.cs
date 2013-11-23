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

		ExecuteScript("CycleBuild");

		if (!IsError)
			ExecuteScript("Release", "src");

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

		var tmpDir = GetTmpDir();

		Unzip(latestFile, tmpDir);

		Console.WriteLine("");
		Console.WriteLine("Tmp dir:");
		Console.WriteLine(" " + tmpDir);
		Console.WriteLine("");

		var subDir = GetNewestFolder(tmpDir); 

		// Move from the sub directory to the intended directory
		MoveDirectory(
			subDir,
			tmpDir
		);

		var originalProjectDirectory = ProjectDirectory;

		// Move to the tmp directory
		ProjectDirectory = tmpDir;

		Console.WriteLine("");
		Console.WriteLine("Preparing...");
		Console.WriteLine("");

		InitializeProject(ProjectDirectory);

		if (!IsError)
		{
			Console.WriteLine("");
			Console.WriteLine("Building...");
			Console.WriteLine("");

			ExecuteScript("CycleBuild");
		}

		//Directory.Delete(ProjectDirectory, true);

		// Move back to the original project directory
		ProjectDirectory = originalProjectDirectory;
	}
}
