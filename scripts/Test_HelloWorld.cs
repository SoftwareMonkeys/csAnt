//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;

class Test_HelloWorldScript : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_HelloWorldScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
	        Grabber.GrabOriginalScriptingFiles();
	
		ExecuteScript("HelloWorld");

		return !IsError;
	}
}
