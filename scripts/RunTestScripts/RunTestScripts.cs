//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;

class RunTestScripts : BaseScript
{
	public static void Main(string[] args)
	{
		new RunTestScripts().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		RunAllTestScripts();

		return !IsError;
	}

	/// <summary>
	/// Runs all the scripts with the "Test_" prefix.
	/// </summary>
	public void RunAllTestScripts()
	{
		// Don't stop if a test fails
		StopOnFail = false;

		var dir = CurrentDirectory;

		var scriptsDir = dir
			+ Path.DirectorySeparatorChar
			+ "scripts";

		Console.WriteLine("Scripts dir:");
		Console.WriteLine(scriptsDir);

		List<string> failedTests = new List<string>();

		// Loop through the test script files
		foreach (var scriptFile in Directory.GetFiles (scriptsDir, "Test_*.cs"))
		{
			// Run the test script
			var passed = RunTestScript(scriptFile);

			// If the test failed to pass then add it to the failed list
			if (!passed)
				failedTests.Add(scriptFile);

			// Set the current directory back to the original (instead of the tmp folder)
			CurrentDirectory = dir;
		}

		Console.WriteLine ("----------------------------------------");
		
		Console.WriteLine("");
		Console.WriteLine("Finished running test scripts.");
		Console.WriteLine ("");

		// If any tests failed then output it
		if (failedTests.Count > 0)
		{
			Console.WriteLine ("The following " + failedTests.Count + " test(s) failed:");

			// Loop through the list of failed tests
			foreach (var scriptFile in failedTests)
			{
				// Output the name of the failed test
				Console.WriteLine(
					Path.GetFileNameWithoutExtension(scriptFile)
				);
			}

			IsError = true;
		}
		else
		{
			Console.WriteLine ("All test scripts passed successfully.");
		}
	}

	private bool RunTestScript(string scriptFile)
	{
		var passed = false;

		var scriptName = Path.GetFileNameWithoutExtension(scriptFile);

		try
		{
			ExecuteScript(scriptName);
			
			if (IsError)
			{
				Console.WriteLine ("Test failed!");
			}
			else
			{
				Console.WriteLine ("Test succeeded!");
				passed = true;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Test failed!");

			Console.WriteLine ();
			Console.WriteLine (ex.ToString());
		}

		return passed;
	}
}
