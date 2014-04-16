//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;

class Test_ReleaseScript : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_ReleaseScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        FilesGrabber.GrabOriginalFiles();
	        
	        ExecuteScript("CycleBuild", "-mode:all");
	        
	        if (!IsError)
		  ExecuteScript("Release");

		return !IsError;
	}
}
