//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SetUp.dll;
//css_ref ../lib/NUnit.2.6.0.12051/lib/nunit.framework.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting.Live;
using SoftwareMonkeys.csAnt.SetUp;
using NUnit.Framework;

class Test_Live_Update : BaseLiveProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_Live_Update().Start(args);
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
        updater.PackageName = pkgName;
        updater.Update();

        CheckModifiedFile();

		if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld"); 

		return !IsError;
	}

    public void Install(string pkgName)
    {
        var installer = new Installer();
        installer.PackageName = pkgName;
        installer.Version = new Version("0.4.0.100");
        installer.Overwrite = true;
        installer.Install();
    }

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

        Console.WriteLine("Modifying file:");
        Console.WriteLine(file);

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
}
