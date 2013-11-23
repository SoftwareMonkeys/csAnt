//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/NUnit/bin/nunit.framework.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;

class RunScriptTestsScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RunScriptTestsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
            CompileScripts();
            
            RunScriptTests();
        

            return !IsError;
	}
	
	public void RunScriptTests()
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
