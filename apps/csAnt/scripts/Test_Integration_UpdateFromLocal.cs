//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.Scripting.dll";
//css_ref "nunit.framework.dll";

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
class Test_Integration_UpdateFromLocal : BaseTestScript
{
    public string TestSourceDirectory = String.Empty;
    public string TestProjectDirectory = String.Empty;

	public static void Main(string[] args)
	{
            new Test_Integration_UpdateFromLocal().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalFiles();

        ExecuteScript("UpdateFromLocal");
        /*TestSourceDirectory = CurrentDirectory;

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

        Assert.IsTrue(newContent.Contains("Hello universe"), "The new content wasn't found.");*/

//        ExecuteScript("Update");

   /*     Assert.IsTrue(!IsError, "An error occurred.");

        Assert.IsTrue(ConsoleWriter != null, "Console is null");
        
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
            "UpdateFromLocal",
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
