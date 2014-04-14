//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting.Live;

class Test_SetUpScript : BaseLiveProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_SetUpScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		// TODO: Better organize this script

		Console.WriteLine("");
		Console.WriteLine("Testing setup (live)...");
		Console.WriteLine("");

		var setUpExeFile = OriginalDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		var setUpExeToFile = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		File.Copy(setUpExeFile, setUpExeToFile, true);

        // TODO: Add windows support by running a vbs equivalent
		StartProcess("mono", setUpExeToFile, "-i:false");

		if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld"); 

		return !IsError;
	}
}
