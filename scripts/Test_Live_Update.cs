//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SetUp.Common.dll;
//css_ref ../lib/NUnit/bin/nunit.framework.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.SetUp.Common;
using NUnit.Framework;

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
		Console.WriteLine("Testing update from web...");
		Console.WriteLine("");

        // TODO: Should this be removed so the download part is tested?
        // Grab the nuget.exe file from the original project directory so it doesn't need to be downloaded
        new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalFiles(
            "lib/nuget.exe"
        );

        var pkgName = "csAnt";

        Install(pkgName);

        ModifyFile();

        var updater = new Updater();
        updater.Update(pkgName, true);

        CheckModifiedFile();




		// TODO: Launch a http server and test against it, to remove the dependency of having a live server

		/*StartHttp(
			CurrentDirectory,
			"localhost",
			8089
		);

		StartNewProcess("http://localhost:8089/readme.txt");

		Thread.Sleep(5000);*/

        // TODO: Rename
		/*var setUpExeFile = OriginalDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-setup.sh";

		var setUpExeToFile = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-setup.sh";

		File.Copy(setUpExeFile, setUpExeToFile, true);

        // TODO: Add windows support by running a vbs equivalent
		StartProcess("bash", setUpExeToFile, "-i:false");*/

		if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld"); 

		return !IsError;
	}

    public void Install(string pkgName)
    {
        var installer = new Installer();
        installer.Install(pkgName, new Version("0.3.0.8900"), true);
    }

    /*public void Prepare(string version)
    {
        MockInstall(version);

        var script = GetDummyScript();
        script.CreateNodes();
    }*/

    public void CheckModifiedFile()
    {
        var file = GetHelloWorldFile();

        var content = File.ReadAllText(file);

        // Check that the content no longer contains "ModifiedText" because it should have been updated
        Assert.IsFalse(
            content.Contains("ModifiedText"),
            "The file content must not have been updated."
            );
    }

    public void ModifyFile()
    {
        var file = GetHelloWorldFile();

        var content = File.ReadAllText(file);

        content.Replace("Hello world", "ModifiedText");

        File.WriteAllText(file, content);
    }

    public string GetHelloWorldFile()
    {
        return CurrentDirectory
            + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + "HelloWorld.cs";
    }
/*
    public void MockInstall(string version)
    {
        // TODO: Move the files directly without using the installer, because this is a unit test and should only be relying on one component

        var installer = new Installer();

        // Assign mock nuget components to avoid actually using nuget during the test (fas
        installer.NugetChecker = CreateMockNugetChecker(false);
        installer.NugetExecutor = CreateMockNugetExecutor(version);

        installer.Install();
    }*/
}
