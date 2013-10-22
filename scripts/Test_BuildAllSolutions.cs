//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;

class Test_BuildAllSolutionsScript : BaseProjectTestScript
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
