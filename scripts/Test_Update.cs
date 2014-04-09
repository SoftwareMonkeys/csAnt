//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/NUnit.2.6.3/lib/nunit.framework.dll

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using NUnit.Framework;

class Test_StartProcessScript : BaseTestScript
{
	public static void Main(string[] args)
	{
            new Test_StartProcessScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        new FilesGrabber(
		    OriginalDirectory,
		    CurrentDirectory
	    ).GrabOriginalScriptingFiles();

        ExecuteScript("Update");

        Assert.IsTrue(!IsError, "An error occurred.");

        Assert.IsTrue(ConsoleWriter != null, "Console is null");
        
        Assert.IsTrue(ConsoleWriter.Output != null, "Console.Output is null");
        
        Assert.IsTrue(ConsoleWriter.Output.Contains("Update complete!"), "Invalid output.");

        return !IsError;
	}
}
