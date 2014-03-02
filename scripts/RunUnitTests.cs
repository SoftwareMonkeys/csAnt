//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/NUnit/bin/nunit.framework.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;

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
            var runner = new NUnitTestRunner(
                this,
                "Release"
            );
            
            var dir = CurrentDirectory
                + Path.DirectorySeparatorChar
                + "bin"
                + Path.DirectorySeparatorChar
                + "Release";
            
            runner.RunTestsInDirectory(dir);
	}
}
