//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;

class RunTestsScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RunTestsScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		ExecuteScript("RunUnitTests");

		ExecuteScript("RunTestScripts");

		// TODO: Add functional testing
		return !IsError;
	}

}
