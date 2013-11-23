//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/NUnit/bin/nunit.framework.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
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
	        FilesGrabber.GrabOriginalScriptingFiles();
	
		ExecuteScript("HelloWorld");

		return !IsError;
	}
	
}
