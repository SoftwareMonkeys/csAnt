using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.IO;

class RunTestsScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RunTestsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	    // Compile the scripts so they can run as unit tests
            CompileScripts();
            
            // Run all tests including scripts and standard unit tests
            RunTests();

            return !IsError;
	}
	
	public void RunTests()
	{
		// Move all the files into a new location where the tests can run
		// (without doing this, the scripts don't execute as tests because the general csAnt binaries aren't in the same folder)
		var workingDir = PrepareTests();

            var runner = new NUnitTestRunner(
                this,
                BuildMode
            );
            
            runner.RunTestsInDirectory(workingDir);
	}

	public string PrepareTests()
	{
		var workingDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "bin-tests"
			+ Path.DirectorySeparatorChar
			+ BuildMode;

		var binDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "bin"
			+ Path.DirectorySeparatorChar
			+ BuildMode;

		var scriptsBinDir = binDir
			+ Path.DirectorySeparatorChar
			+ "scripts";

		EnsureDirectoryExists(workingDir);

		foreach (var f1 in Directory.GetFiles(binDir))
		{
			var t1 = workingDir
				+ Path.DirectorySeparatorChar
				+ Path.GetFileName(f1);

			File.Copy(f1, t1, true);
		}

		foreach (var f2 in Directory.GetFiles(scriptsBinDir))
		{
			var t2 = workingDir
				+ Path.DirectorySeparatorChar
				+ Path.GetFileName(f2);

			File.Copy(f2, t2, true);
		}

		return workingDir;
	}
}
