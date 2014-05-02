//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.External.Nuget.dll

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.External.Nuget;

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

        var sourcePath = "https://www.myget.org/F/softwaremonkeys/";

        Console.WriteLine("Feed source path:");
        Console.WriteLine(sourcePath);
        Console.WriteLine("");

        var versioner = new NugetVersioner(sourcePath);

        var status = "";

        if (CurrentNode.Properties.ContainsKey("Status"))
            status = CurrentNode.Properties["Status"];

        var force = Arguments.ContainsAny("f", "force");

        Console.WriteLine("Status: " + (!String.IsNullOrEmpty(status) ? status : "[Not specified]"));
        Console.WriteLine("");

        var currentVersionString = "0.0.0.0";
        if (CurrentNode.Properties.ContainsKey("Version"))
            currentVersionString = CurrentNode.Properties["Version"];
        if (currentVersionString.Contains("-"))
            currentVersionString = currentVersionString.Substring(0, currentVersionString.IndexOf("-"));
        var currentVersion = new Version(currentVersionString);

        var publishedVersion = versioner.GetVersion("csAnt", status);

        Console.WriteLine("Current version: " + currentVersion);
        Console.WriteLine("Published version: " + publishedVersion);
        Console.WriteLine("");

        if (publishedVersion != currentVersion)
        {
            if (force
                || publishedVersion > currentVersion)
            {
                if (force)
                    Console.WriteLine("Current version is newer. Overwriting anyway.");
                else
                    Console.WriteLine("Published version is newer.");

                Console.WriteLine("Using published version.");

                ExecuteScript("SetVersion", publishedVersion.ToString());
            }
            else
            {
                Console.WriteLine("Current version is newer.");
                Console.WriteLine("Staying with current version.");
            }
        }
        else
        {
                Console.WriteLine("Current version already matches the published version. Nothing needs to be done.");
        }

        Console.WriteLine("");

        RefreshCurrentNode();

        return !IsError;
	}
}
