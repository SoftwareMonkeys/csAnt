//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.IO.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.IO;

class Test_Release_SpecifyList_Script : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_Release_SpecifyList_Script().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalFiles();
        
        ExecuteScript("CycleBuild");
        
        if (!IsError)
		    ExecuteScript("Package", "csAnt");

		if (Summaries.Count > 1)
			Error("Multiple packages were generated when only one should have been.");

		return !IsError;
	}
}
