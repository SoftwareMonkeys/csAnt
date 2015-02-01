//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.Scripting.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll";

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_BuildAllSolutionsScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_BuildAllSolutionsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		new FilesGrabber(
			OriginalDirectory,
			CurrentDirectory
		).GrabOriginalFiles();
		
		ExecuteScript("BuildAllSolutions");

		return !IsError;
	}
}
