//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.External.Nuget.Tests.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock;

class Test_Integration_SetUpConsole_Import : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_Integration_SetUpConsole_Import().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		// TODO: Better organize this script

		Console.WriteLine("");
		Console.WriteLine("Testing setup from web...");
		Console.WriteLine("");

        var feedPath = Path.GetDirectoryName(CurrentDirectory)
            + Path.DirectorySeparatorChar
            + "TestFeed";

        CreateNodes();

        new MockNugetFeedCreator(CurrentDirectory, feedPath).Create();

		var setUpExeFile = OriginalDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		var setUpExeToFile = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		File.Copy(setUpExeFile, setUpExeToFile, true);

		StartDotNetExe(
            setUpExeToFile,
            "-intro=false",
            "-source=" + feedPath,
            "-nuget=" + feedPath,
            "-import"
        );

		if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld"); 

		return !IsError;
	}
}
