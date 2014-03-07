//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.IO.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.IO.Contracts.dll;
//css_ref ../lib/NUnit/bin/nunit.framework.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.IO;
using NUnit.Framework;

[TestFixture]
public class Test_HelloWorldScript : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_HelloWorldScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        new FilesGrabber(
                    OriginalDirectory,
                    CurrentDirectory,
		    		IsVerbose
                ).GrabOriginalScriptingFiles();
	
		ExecuteScript("HelloWorld");

		Assert.IsTrue(!IsError);

		return !IsError;
	}
	
}
