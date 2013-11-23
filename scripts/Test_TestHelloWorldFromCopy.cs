//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_TestHelloWorldFromCopyScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_TestHelloWorldFromCopyScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Running Test_HelloWorld from within a copy of the project...");
		Console.WriteLine("");
		
                FilesGrabber.GrabOriginalFiles();
                
                var script = GetScript("Test_HelloWorld");
                
                script.Start();
		
		return !IsError;
	}
}
