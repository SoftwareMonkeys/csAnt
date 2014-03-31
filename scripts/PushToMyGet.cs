using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class PushToMyGetScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new PushToMyGetScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var pkgsDir = CurrentDirectory
            + Path.DirectorySeparatorChar
            + "pkg";

        Console.WriteLine("Packages:");
        Console.WriteLine(" " + pkgsDir);
        Console.WriteLine();

        foreach (var dir in Directory.GetDirectories(pkgsDir))
        {
            PushPackagesInDirectory(dir);
        }

		return true;
	}

    public void PushPackagesInDirectory(string dir)
    {
        var destination = GetMyGetDestination();

        var pkgFile = GetNewestFile(dir);

        pkgFile = ToRelative(pkgFile);

        Console.WriteLine("Package file:");
        Console.WriteLine(" " + pkgFile);

        var apiKey = CurrentNode.Nodes["Security"].Nodes["MyGet"].Properties["ApiKey"];

        var arguments = "push"
            + " " + pkgFile
            + " " + apiKey
            + " -Source " + destination;

        ExecuteScript("nuget", arguments);
    }

    public string GetMyGetDestination()
    {
        return "https://www.myget.org/F/csant/api/v2/package";
    }
}
