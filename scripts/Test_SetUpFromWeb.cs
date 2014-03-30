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

class Test_SetUpFromWebScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_SetUpFromWebScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		// TODO: Better organize this script

		Console.WriteLine("");
		Console.WriteLine("Testing setup from web...");
		Console.WriteLine("");

		// TODO: Launch a http server and test against it, to remove the dependency of having a live server

		/*StartHttp(
			CurrentDirectory,
			"localhost",
			8089
		);

		StartNewProcess("http://localhost:8089/readme.txt");

		Thread.Sleep(5000);*/

		var setUpExeFile = OriginalDirectory
			+ Path.DirectorySeparatorChar
			+ "bin"
			+ Path.DirectorySeparatorChar
			+ "Release"
			+ Path.DirectorySeparatorChar
			+ "packed"
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		var setUpExeToFile = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		File.Copy(setUpExeFile, setUpExeToFile, true);

		StartProcess(setUpExeToFile, "-i:false");

		if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld"); 

		return !IsError;
	}
}
