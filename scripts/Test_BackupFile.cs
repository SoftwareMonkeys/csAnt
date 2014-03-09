//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;

class Test_BackupFileScript : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_BackupFileScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        new FilesGrabber(
			OriginalDirectory,
			CurrentDirectory
		).GrabOriginalScriptingFiles();
	        
		ExecuteScript("BackupFile", "csAnt.node");

		// TODO: Check that the backup file exists

		return !IsError;
	}
}
