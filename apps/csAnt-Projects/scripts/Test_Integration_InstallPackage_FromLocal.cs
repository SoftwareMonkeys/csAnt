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

class Test_Integration_InstallPackage_FromLocal : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_Integration_InstallPackage_FromLocal().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		// TODO: Better organize this script

		Console.WriteLine("");
		Console.WriteLine("Testing package installation...");
		Console.WriteLine("");

        new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalFiles();

        ExecuteScript("EnsurePackage", "csAnt");

        var testDirectory = Path.GetDirectoryName(CurrentDirectory)
            + Path.DirectorySeparatorChar
            + "TestProject";

        new FileCopier(
            CurrentDirectory,
            testDirectory
        ).Copy(
            "lib/nuget.exe"
        );

        var sourceFeed = CurrentDirectory
            + Path.DirectorySeparatorChar
            + "pkg";

        Relocate(testDirectory);

        StartDotNetExe(
            "lib/nuget.exe",
            "install",
            "csAnt",
            "-Source " + sourceFeed,
            "-NoCache"
        );


		/*if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld"); */

		return !IsError;
	}
}
