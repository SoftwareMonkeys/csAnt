//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;

class Test_Release_SpecifyList_Script : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_Release_SpecifyList_Script().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		ExecuteScript("Release", "bin");

		if (Summaries.Count > 1)
			Error("Multiple releases were generated when only one should have been.");

		return !IsError;
	}
}
