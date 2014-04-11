//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/NUnit.2.6.0.12051/lib/nunit.framework.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;

class RunUnitTestsScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RunUnitTestsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
            // Run tests
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
