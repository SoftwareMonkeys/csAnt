using System;
using System.IO;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.External.Nuget;

class UpdateLib : BaseScript
{
	public static void Main(string[] args)
	{
		new UpdateLib().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var id = args[0];
        var version = "0.0.0.0";
        var status = "";
        var sourcePath = "https://www.myget.org/F/softwaremonkeys/";
        var defaultSourcePath = "https://go.microsoft.com/fwlink/?LinkID=206669";

        if (Arguments.ContainsAny("v", "version"))
            version = Arguments["v", "version"];

        if (Arguments.ContainsAny("source"))
            sourcePath = Arguments["source"];

        if (Arguments.ContainsAny("status"))
            status = Arguments["status"];

        Console.WriteLine("");
        Console.WriteLine("Updating library...");
        Console.WriteLine("");
        Console.WriteLine("Specified version: " + (!String.IsNullOrEmpty(version) ? version : "[Latest]"));
        Console.WriteLine("");
        Console.WriteLine("Status: " + (!String.IsNullOrEmpty(status) ? status : "[Stable Release]"));
        Console.WriteLine("");
        Console.WriteLine("Source: " + sourcePath);
        Console.WriteLine("");

        var versioner = new NugetVersioner();
        versioner.NugetSourcePath = sourcePath;
        var foundVersion = versioner.GetVersion(id, new Version(version), status);

        Console.WriteLine("Found version: " + (foundVersion != null && foundVersion > new Version(0,0,0,0) ? foundVersion.ToString() : "[Latest]"));
        Console.WriteLine("");

        var executor = new NugetExecutor();

        var list = new List<string>();
        list.Add("install");
        list.Add(id);
        list.Add("-Source " + sourcePath);
        list.Add("-Source " + defaultSourcePath);
        list.Add("-OutputDirectory lib"); // TODO: Make this configurable
        list.Add("-NoCache");

        if (foundVersion != null
            && foundVersion > new Version(0,0,0,0))
        {
            list.Add("-Version " + foundVersion);
            list.Add("-Pre");
        }

        executor.Execute(
            list.ToArray()
        );
            
        ExecuteScript("SetLibVersion", id, foundVersion.ToString());

        new FileCopier(
            ToAbsolute("lib"),
            ToAbsolute("pkg")
        ).Copy(
            "*.nupkg",
            "!" + CurrentNode.Name
        );

		return !IsError;
	}
}
