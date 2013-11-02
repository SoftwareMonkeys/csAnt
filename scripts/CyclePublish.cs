//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CyclePublishScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CyclePublishScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full release cycle.");
		Console.WriteLine("");

		// Build the whole project first
		ExecuteScript("CycleBuild");

		// Git clone the project to another directory
		var tmpDir = CloneToTmpDirectory();

		// Prepare the project (eg. download libs)
		InitializeProject(tmpDir);

		// Move to the cloned directory
		CurrentDirectory = tmpDir;

		// Build the cloned source code
		ExecuteScript("CycleBuild");

                if (!IsError)
                {
		        Console.WriteLine("");
		        Console.WriteLine("Creating release zip files...");
		        Console.WriteLine("");

		        // Create the release
		        ExecuteScript(
			        "Release",
			        new string[]{
				        "-mode:Release"
			        }
		        );
                }

		if (!IsError)
		{
			Console.WriteLine("Uploading the release zip file to GoogleCode...");
			Console.WriteLine("");

			// Upload to GoogleCode
			ExecuteScript("GoogleCodeRelease");
		}

		return !IsError;
	}

	public string CloneToTmpDirectory()
	{
		Console.WriteLine("Cloning to tmp directory...");

		var originalDirectory = CurrentDirectory;

		var tmpDirectory = GetTmpDir();

		Console.WriteLine("Tmp directory:");
		Console.WriteLine(tmpDirectory);

		Directory.CreateDirectory(tmpDirectory);

		GitClone(ProjectDirectory, tmpDirectory);

		AddSummary("Cloned project to: " + tmpDirectory);

		CopySecurityCode(originalDirectory, tmpDirectory);

		return tmpDirectory;
	}

	public void CopySecurityCode(string fromDir, string toDir)
	{
		var fromFile = fromDir
			+ Path.DirectorySeparatorChar
			+ "_security"
			+ Path.DirectorySeparatorChar
			+ "GoogleCode"
			+ Path.DirectorySeparatorChar
			+ "GoogleCode.node";

		var toFile = fromFile.Replace(fromDir, toDir);

                Console.WriteLine("Copying security node to:");
                Console.WriteLine(toFile);
                Console.WriteLine("From:");
                Console.WriteLine(fromFile);

		EnsureDirectoryExists(Path.GetDirectoryName(toFile));

		File.Copy(fromFile, toFile);
	}
}
