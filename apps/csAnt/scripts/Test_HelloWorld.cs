//css_ref ../../../lib/NUnit.2.6.0.12051/lib/nunit.framework.dll;

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
