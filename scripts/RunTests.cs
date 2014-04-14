//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/NUnit.2.6.0.12051/lib/nunit.framework.dll;

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
        Console.WriteLine("");
        Console.WriteLine("Running tests...");
        Console.WriteLine("");

        var binTestsDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "bin-tests";

        Console.WriteLine("Bin tests dir:");
        Console.WriteLine(binTestsDir);

		// Move all the files into a new location where the tests can run
		// (without doing this, the scripts don't execute as tests because the general csAnt binaries aren't in the same folder)
		var workingDir = PrepareTests(binTestsDir);

        var runner = new NUnitTestRunner(
            this,
            BuildMode
        );

        // Exclude live tests during standard testing because most of the time they're not required and too slow during development.
        // They should be performed on their own when required
        runner.AddExcludeCategory("Live"); 
    
        runner.RunTestsInDirectory(workingDir);


		Directory.Delete(binTestsDir, true);
	}

	public string PrepareTests(string binTestsDir)
	{
		var workingDir = binTestsDir
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

        Console.WriteLine("Copying bin files to test bin directory:");

		foreach (var f1 in Directory.GetFiles(binDir))
		{
			var t1 = workingDir
				+ Path.DirectorySeparatorChar
				+ Path.GetFileName(f1);

            Console.WriteLine(ToRelative(t1));

			File.Copy(f1, t1, true);
		}

		foreach (var f2 in Directory.GetFiles(scriptsBinDir))
		{
			var t2 = workingDir
				+ Path.DirectorySeparatorChar
				+ Path.GetFileName(f2);

            Console.WriteLine(ToRelative(t2));
			File.Copy(f2, t2, true);
		}

		return workingDir;
	}
}
