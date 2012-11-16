//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class TestBuildFromSourceReleaseScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new TestBuildFromSourceReleaseScript().Start();
	}
	
	public void Start()
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from the release files...");
		Console.WriteLine("");

		ExecuteScript("CycleBuild");

		ExecuteScript("Release");

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
		
	}

	public void UnzipAndBuild(string latestFile)
	{
		var tmpDir = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "_tmp"
			+ Path.DirectorySeparatorChar
			+ Guid.NewGuid().ToString()
			+ Path.DirectorySeparatorChar
			+ ProjectName;

		Unzip(latestFile, tmpDir);

		// Move from the sub directory to the intended directory
		MoveDirectory(
			GetNewestFolder(tmpDir),
			tmpDir
		);

		ProjectDirectory = tmpDir;

		PrepareProject(ProjectDirectory);

		if (!IsError)
			Build(ProjectDirectory);

		Directory.Delete(ProjectDirectory, true);
	}

	public void Build(string projectDirectory)
	{
		var slnFiles = Directory.GetFiles(
			projectDirectory,
			ProjectName + ".MonoDevelop.sln",
			SearchOption.AllDirectories
		);

		string slnFile = String.Empty;

		if (slnFiles.Length > 0)
		{
			slnFile = slnFiles[0];

			BuildSolution(slnFile);
		}
		else
		{
			Console.WriteLine("No .sln files found in:");
			Console.WriteLine(projectDirectory);
		}
	}
}
