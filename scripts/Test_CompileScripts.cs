//css_ref ../lib/NUnit.2.6.3/lib/nunit.framework.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using NUnit.Framework;

class Test_CompileScriptsScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_CompileScriptsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        Console.WriteLine("");
        Console.WriteLine("Testing whether all the scripts can compile...");
        Console.WriteLine("");

        new FilesGrabber(
	        OriginalDirectory,
	        CurrentDirectory
        ).GrabOriginalScriptingFiles();

        CompileScripts(true);

        Assert.IsFalse(IsError, "An error occurred.");
		
		return !IsError;
	}

}
