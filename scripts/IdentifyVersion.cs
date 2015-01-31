//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.External.Nuget.dll
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Versions.dll
//css_ref ../lib/Nuget.Core.2.8.1/lib/net40-Client/NuGet.Core.dll


using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Versions;
using NuGet;

class IdentifyVersion : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new IdentifyVersion().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        Console.WriteLine("");
        Console.WriteLine("Identifying version by checking NuGet feed and git origin repository...");
        Console.WriteLine("");

        var packageName = ProjectName;

        var sourcePath = CurrentNode.Properties["NuGetFeedPath"];

        Console.WriteLine("Feed source path:");
        Console.WriteLine(sourcePath);
        Console.WriteLine("");
        
        var commit = Arguments.ContainsAny("c", "commit");
        
        var status = "";

        if (CurrentNode.Properties.ContainsKey("Status"))
            status = CurrentNode.Properties["Status"];

        var branch = "";
        
        if (CurrentNode.Properties.ContainsKey("Branch"))
            branch = CurrentNode.Properties["Branch"];

        var identifier = new VersionIdentifier(branch, status, sourcePath, packageName);


        var force = Arguments.ContainsAny("f", "force");

        Console.WriteLine("Status: " + (!String.IsNullOrEmpty(status) ? status : "[Not specified]"));
        Console.WriteLine("");

        var currentVersionString = "0.0.0.0";
        if (CurrentNode.Properties.ContainsKey("Version"))
            currentVersionString = CurrentNode.Properties["Version"];
            
        currentVersionString += "-" + status;
            
        var currentVersion = new SemanticVersion(currentVersionString);

        var publishedVersion = identifier.GetVersion();

        Console.WriteLine("");
        Console.WriteLine("Current local version: " + currentVersion);
        Console.WriteLine("Latest found version: " + publishedVersion);
        Console.WriteLine("");
        
        bool changedVersion = false;

        if (publishedVersion != currentVersion)
        {
            if (force
                || publishedVersion > currentVersion)
            {
                if (force)
                    Console.WriteLine("Current local version is newer. Overwriting anyway.");
                else
                    Console.WriteLine("Lastest found version is newer.");

                Console.WriteLine("Using latest found version.");

                ExecuteScript("SetVersion", publishedVersion.Version.ToString());
                
                changedVersion = true;
            }
            else
            {
                Console.WriteLine("Current local version is newer. Keeping.");
            }
        }
        else
        {
                Console.WriteLine("Current version already matches the published version. Nothing needs to be done.");
        }

        Console.WriteLine("");

        RefreshCurrentNode();
        
        if (commit && changedVersion)
            ExecuteScript("CommitVersion");

        return !IsError;
	}
}
