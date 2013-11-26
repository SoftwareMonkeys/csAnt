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
		new TestBuildFromStandardReleaseScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        FilesGrabber.GrabOriginalFiles();
	        
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
				+ "standard-release";

			var latest = GetNewestFile(rlsDir);

			UnzipAndPrepare(latest);
		}

		return !IsError;
		
	}

	public void UnzipAndPrepare(string latestFile)
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

		// Run the prepare scripts
		PrepareProject(ProjectDirectory);

		// Delete the temporary directory
		Directory.Delete(tmpDir, true);
	}
}
