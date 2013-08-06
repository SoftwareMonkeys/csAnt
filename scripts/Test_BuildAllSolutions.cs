//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class Test_BuildAllSolutionsScript : BaseScript
{
	public static void Main(string[] args)
	{
		new Test_BuildAllSolutionsScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		ExecuteScript("BuildAllSolutions");

		return !IsError;
	}
}
