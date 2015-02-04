//css_ref "SoftwareMonkeys.csAnt.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.Scripting.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll";
//css_ref "SoftwareMonkeys.csAnt.External.Nuget.Tests.dll";

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.External.Nuget.Tests.Mock;

class Test_Integration_SetUpConsole : BaseProjectTestScript
{
    public static string FeedPath { get;set; }

    public static string TestSourceDirectory { get;set; }

    public static string TestProjectDirectory { get;set; }

	public static void Main(string[] args)
	{
		new Test_Integration_SetUpConsole().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		// TODO: Better organize this script

		Console.WriteLine("");
		Console.WriteLine("Testing setup from web...");
		Console.WriteLine("");

        Prepare();

        Relocate(TestProjectDirectory);

        var nugetPath = Path.Combine(TestSourceDirectory, "lib/nuget.exe");

		StartDotNetExe(
            "csAnt-SetUp.exe",
            "-intro=false",
            "-source=" + FeedPath,
            "-nuget=" + nugetPath
        );

		if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld"); 

		return !IsError;
	}

    public void Prepare()
    {
        TestSourceDirectory = CurrentDirectory;

        TestProjectDirectory = Path.GetDirectoryName(CurrentDirectory)
            + Path.DirectorySeparatorChar
            + "TestProject";

        EnsureDirectoryExists(TestProjectDirectory);

        FeedPath = Path.GetDirectoryName(CurrentDirectory)
            + Path.DirectorySeparatorChar
            + "pkgs";

        CreateNodes();

        new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalFiles();

        ExecuteScript("EnsureBuild", "csAnt");

        // TODO: Should a feed be created normally instead of a mock feed?
        new MockNugetFeedCreator(OriginalDirectory, CurrentDirectory, FeedPath).Create();

		var setUpExeFile = TestSourceDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		var setUpExeToFile = TestProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		File.Copy(setUpExeFile, setUpExeToFile, true);
    }
}
