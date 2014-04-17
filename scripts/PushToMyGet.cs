using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Linq;

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

        var pkgFile = GetLatestPackage(dir);

        if (!String.IsNullOrEmpty(pkgFile))
        {
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
        else
            Console.WriteLine("No package file. Skipping " + Path.GetFileName(dir));
    }

    public string GetMyGetDestination()
    {
        return "https://www.myget.org/F/softwaremonkeys/api/v2/package";
    }

    static public string GetLatestPackage(string directory)
    {
        string file = String.Empty;

        var files = new DirectoryInfo(directory).GetFiles().OrderByDescending(p => p.Name);

        foreach (var f in files)
        {
            file = f.FullName;
            break;
        }

        return file;
    }
}
