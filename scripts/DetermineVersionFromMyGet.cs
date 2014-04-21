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
            "-Source \"" + feedPath + "\"",
            "-OutputDirectory \"" + tmpDir + "\"",
            "-NoCache",
            "-Pre"
        );

        var dir = Directory.GetDirectories(tmpDir, "csAnt.*")[0];

        var publishedVersionString = Path.GetFileName(dir).Replace("csAnt.", "");
        if (publishedVersionString.Contains("-"))
            publishedVersionString = publishedVersionString.Substring(0, publishedVersionString.IndexOf("-"));
        var publishedVersion = new Version(publishedVersionString);

        var currentVersionString = CurrentNode.Properties["Version"];
        if (currentVersionString.Contains("-"))
            currentVersionString = currentVersionString.Substring(0, currentVersionString.IndexOf("-"));
        var currentVersion = new Version(CurrentNode.Properties["Version"]);

        Console.WriteLine("Current version: " + currentVersion);
        Console.WriteLine("Published version: " + publishedVersion);
        Console.WriteLine("");

        if (publishedVersion > currentVersion)
        {
            Console.WriteLine("Published version is newer.");
            Console.WriteLine("Using published version.");

            ExecuteScript("SetVersion", publishedVersion.ToString());
        }
        else
        {
            Console.WriteLine("Current version is newer.");
            Console.WriteLine("Staying with current version.");
        }

        Console.WriteLine("");

        RefreshCurrentNode();

        return !IsError;
	}
}
