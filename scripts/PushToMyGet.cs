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
        var packageName = "";
        if (args.Length > 0)
            packageName = args[0];

        var pkgsDir = CurrentDirectory
            + Path.DirectorySeparatorChar
            + "pkg";

        Console.WriteLine("Packages:");
        Console.WriteLine(" " + pkgsDir);
        Console.WriteLine();

        if (!String.IsNullOrEmpty(packageName))
        {
            var dir = pkgsDir
                + Path.DirectorySeparatorChar
                + packageName;

            PushPackagesInDirectory(dir);
        }
        else
        {
            foreach (var dir in Directory.GetDirectories(pkgsDir))
            {
                if (IsProjectPackage(Path.GetFileName(dir)))
                    PushPackagesInDirectory(dir);
            }
        }

		return true;
	}

    public bool IsProjectPackage(string packageName)
    {
        return packageName.StartsWith(ProjectName);
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
