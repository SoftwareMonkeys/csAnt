//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_NugetInstallCsAntPackage : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_NugetInstallCsAntPackage().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from git clone...");
		Console.WriteLine("");
		
		new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalFiles();

        string packageName = "csAnt";

        ExecuteScript("CyclePackage", packageName);

        var pkgDir = CurrentDirectory
            + Path.DirectorySeparatorChar
            + "pkg";

        var testProjectDir = Path.GetDirectoryName(CurrentDirectory)
            + Path.DirectorySeparatorChar
            + "TestProject";

        var nugetFile = "lib/nuget.exe";

		new FileCopier(
            CurrentDirectory,
            testProjectDir
        ).Copy(
            nugetFile
        );        

        Relocate(testProjectDir);

        StartDotNetExe(
            "lib/nuget.exe",
            "install",
            packageName,
            "-Source " + pkgDir,
            "-OutputDirectory lib",
            "-NoCache"
        );

		return !IsError;
	}


}
