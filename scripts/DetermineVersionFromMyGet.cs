using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class DetermineVersionFromMyGet : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new DetermineVersionFromMyGet().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        Console.WriteLine("");
        Console.WriteLine("Determining version from MyGet feed...");
        Console.WriteLine("");

        var tmpDir = GetTmpDir();

        var feedPath = "https://www.myget.org/F/softwaremonkeys/";

        Console.WriteLine("Feed path:");
        Console.WriteLine(feedPath);
        Console.WriteLine("");

        Console.WriteLine("The latest version of csAnt will now be downloaded from the feed and installed so the version of it determined...");
        Console.WriteLine("");

        ExecuteScript(
            "Nuget",
            "install",
            "csAnt",
            "-Source " + feedPath,
            "-OutputDirectory " + tmpDir
        );

        var dir = Directory.GetDirectories(tmpDir, "csAnt.*")[0];

        var version = Path.GetFileName(dir).Replace("csAnt.", "");

        Console.WriteLine("Version: " + version);
        Console.WriteLine("");

        CurrentNode.Properties["Version"] = version;

        ExecuteScript("SetVersion", version);

        return !IsError;
	}
}
