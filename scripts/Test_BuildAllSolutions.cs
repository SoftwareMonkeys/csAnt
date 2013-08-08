//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class Test_BuildAllSolutionsScript : BaseProjectScript
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
