//css_ref "SoftwareMonkeys.csAnt.Tests.dll"
//css_ref "SoftwareMonkeys.csAnt.Tests.Scripting.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll";

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting.Live;

class Test_Live_SetUpScript : BaseLiveProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_Live_SetUpScript().Start(args);
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

        // TODO: Rename
		var setUpExeFile = OriginalDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-setup.sh";

		var setUpExeToFile = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-setup.sh";

		File.Copy(setUpExeFile, setUpExeToFile, true);

        // TODO: Add windows support by running a vbs equivalent
		StartProcess("bash", setUpExeToFile, "-i:false");

		if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld"); 

		return !IsError;
	}
}
