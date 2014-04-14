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

class Test_RemoveLibScript : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_RemoveLibScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        new FilesGrabber(
			OriginalDirectory,
			CurrentDirectory
		).GrabOriginalScriptingFiles();
	
		var libName = "TestLib";

		ExecuteScript("AddLib", libName, "http://www.nowhere.com/lib.zip");

		var dir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "lib"
			+ Path.DirectorySeparatorChar
			+ libName;

		var nodePath = dir
			+ Path.DirectorySeparatorChar
			+ libName
			+ ".node";

		if (!Directory.Exists(dir))
		{
			Error("Library directory wasn't created/found (during preparation for test).");
		}

		if (!File.Exists(nodePath))
		{
			Error("Library node wasn't created/found (during preparation for test).");
		}

		ExecuteScript("RemoveLib", libName);

		if (Directory.Exists(dir))
		{
			Error("Library directory wasn't removed.");
		}

		return !IsError;
	}
}
