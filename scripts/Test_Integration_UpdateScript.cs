//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/NUnit.2.6.0.12051/lib/nunit.framework.dll

using System;
using System.IO;
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
    public string Status = String.Empty;

	public static void Main(string[] args)
	{
        new Test_Integration_Update().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        // Prepare a test project source and package
        PrepareSource();

        // Prepare a test installation (installing from the above source) to run the update on
        PrepareInstallation();

        // Modify a source file
        UpdateSourceCode();
        
        // Move back to the test project directory
        Relocate(TestProjectDirectory);

        // Run the update
        RunUpdate();
        
        // Check the results
        CheckUpdate();

        return !IsError;
	}

    public void CheckUpdate()
    {        
        var newHelloWorldFile = Path.Combine(TestProjectDirectory, "scripts/HelloWorld.cs");

        Console.WriteLine("");
        Console.WriteLine("Checking file:");
        Console.WriteLine(newHelloWorldFile);
        Console.WriteLine("");

        var newContent = File.ReadAllText(newHelloWorldFile);

        Assert.IsTrue(newContent.Contains("Hello universe"), "The new content wasn't found.");

   		Assert.IsFalse(IsError, "An error occurred.");
    }

    public void PrepareSource()
    {
        TestSourceDirectory = CurrentDirectory;

        Console.WriteLine("");
        Console.WriteLine("Preparing source project and package to install from...");
        Console.WriteLine("");
        Console.WriteLine("Source project path:");
        Console.WriteLine("  " + TestSourceDirectory);
        Console.WriteLine("");

        new FilesGrabber(
		    OriginalDirectory,
		    CurrentDirectory
	    ).GrabOriginalFiles();

        // Refresh the nodes to pick up the status
        Nodes.Refresh();

        Status = GetStatus();

        ClearSourcePackages();

        PrepareSourcePackage();
    }

    public void ClearSourcePackages()
    {
        foreach (var file in FindFiles(ToAbsolute("pkg"), "**.nupkg"))
        {
            var name = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(file));
            if (!name.Contains(".")) // If there's no . in the name then it's a project package and not a 3rd party package
                File.Delete(file);
        }
    }

    public void PrepareSourcePackage()
    {
        ExecuteScript("CyclePackage", "csAnt");

        Console.WriteLine("");
        Console.WriteLine("Status: " + Status);
        Console.WriteLine("");
    }

    public void PrepareInstallation()
    {
        TestSourceDirectory = CurrentDirectory;

        TestProjectDirectory = Path.GetDirectoryName(CurrentDirectory)
            + Path.DirectorySeparatorChar
            + "TestProject";

        EnsureDirectoryExists(TestProjectDirectory);

        // Relocate to test project directory
        Relocate(TestProjectDirectory);

        // Install the test project from the package
        InstallTestProject(TestSourceDirectory, TestProjectDirectory);

        Relocate(TestSourceDirectory);
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
            "-Nuget=" + nugetPath,
            "-Status=" + Status
        );

        return testProjectDir;
    }

    public void UpdateSourceCode()
    {
        // Modify the HelloWorld script in the source project to see if it gets updated
        ModifyHelloWorldScript();

        ExecuteScript("IncrementVersion", "3");

        // Refresh the nodes to pick up the new version
        Nodes.Refresh();

        // Repackage
        Repackage();
    }

    public void ModifyHelloWorldScript()
    {
        var helloWorldFile = Path.Combine(TestSourceDirectory, "scripts/HelloWorld.cs");

        var content = File.ReadAllText(helloWorldFile);

        Console.WriteLine("");
        Console.WriteLine("Modifying file:");
        Console.WriteLine(helloWorldFile);
        Console.WriteLine("");

        var from = "Hello world";
        var to = "Hello universe";

        Console.WriteLine("Replacing:");
        Console.WriteLine(from);
        Console.WriteLine("With:");
        Console.WriteLine(to);
        Console.WriteLine("");

        content = content.Replace(from, to);

        File.WriteAllText(helloWorldFile, content);
    }

    public void Repackage()
    {
        ExecuteScript("Package", "csAnt");
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

    public string GetStatus()
    {
        if (CurrentNode.Properties.ContainsKey("Status"))
            return CurrentNode.Properties["Status"];
        return "";
    }
}
