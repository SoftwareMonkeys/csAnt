//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/NUnit.2.6.0.12051/lib/nunit.framework.dll

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using NUnit.Framework;

[TestFixture]
class Test_Integration_Update : BaseTestScript
{
    public string TestSourceDirectory = String.Empty;
    public string TestProjectDirectory = String.Empty;

	public static void Main(string[] args)
	{
            new Test_Integration_Update().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        TestSourceDirectory = CurrentDirectory;

        // Prepare a test package
        PreparePackage();

        TestProjectDirectory = Path.GetDirectoryName(CurrentDirectory)
            + Path.DirectorySeparatorChar
            + "TestProject";

        EnsureDirectoryExists(TestProjectDirectory);

        // Relocate to test project directory
        Relocate(TestProjectDirectory);

        // Install the test project from the package
        InstallTestProject(TestSourceDirectory, TestProjectDirectory);

        Relocate(TestSourceDirectory);

        // Modify the HelloWorld script
        ModifyFile();

        // Repackage
        ExecuteScript("CyclePackage", "csAnt");
        
        // Move back to the test project directory
        Relocate(TestProjectDirectory);

        // Run the update
        RunUpdate();
        
        var newHelloWorldFile = Path.Combine(TestProjectDirectory, "scripts/HelloWorld.cs");

        var newContent = File.ReadAllText(newHelloWorldFile);

   		Assert.IsFalse(IsError, "An error occurred.");

        Assert.IsTrue(newContent.Contains("Hello universe"), "The new content wasn't found.");

		// TODO: Check if needed
        /*Assert.IsTrue(ConsoleWriter != null, "Console is null");
        
        Assert.IsTrue(ConsoleWriter.Output != null, "Console.Output is null");
        
        Assert.IsTrue(ConsoleWriter.Output.Contains("Update complete!"), "Invalid output.");*/

        return !IsError;
	}

    public void PreparePackage()
    {
        new FilesGrabber(
		    OriginalDirectory,
		    CurrentDirectory
	    ).GrabOriginalFiles();

        ExecuteScript("CyclePackage", "csAnt");
    }

    public string InstallTestProject(string testDir, string testProjectDir)
    {
        new FileCopier(
            testDir,
            testProjectDir
        ).Copy(
            "csAnt-SetUp.exe"
        );

        var packageDir = GetPackageDir();

        var nugetPath = GetNugetPath();

        StartDotNetExe(
            "csAnt-SetUp.exe",
            "-Source=" + packageDir,
            "-Nuget=" + nugetPath
        );

        return testProjectDir;
    }

    public void ModifyFile()
    {
        var helloWorldFile = Path.Combine(TestSourceDirectory, "scripts/HelloWorld.cs");

        var content = File.ReadAllText(helloWorldFile);

        content = content.Replace("Hello world", "Hello universe");

        File.WriteAllText(helloWorldFile, content);
    }

    public void RunUpdate()
    {
        var packageDir = GetPackageDir();

        var nugetPath = GetNugetPath();

        ExecuteScript(
            "Update",
            "-Source=" + packageDir,
            "-Nuget=" + nugetPath
        );
    }

    public string GetPackageDir()
    {
        return TestSourceDirectory
            + Path.DirectorySeparatorChar
            + "pkg";
    }

    public string GetNugetPath()
    {
        return Path.Combine(TestSourceDirectory, "lib/nuget.exe");
    }
}
