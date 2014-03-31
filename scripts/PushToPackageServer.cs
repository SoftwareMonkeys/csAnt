using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class PushToPackageServerScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new PushToPackageServerScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var pkgsDir = CurrentDirectory
            + Path.DirectorySeparatorChar
            + "pkg";

        Console.WriteLine("Packages:");
        Console.WriteLine(" " + pkgsDir);
        Console.WriteLine();

        var buildServerPath = GetBuildServerPath();

        Console.WriteLine("Build server:");
        Console.WriteLine(" " + buildServerPath);
        Console.WriteLine();

        foreach (var dir in Directory.GetDirectories(pkgsDir))
        {
            PushPackagesInDirectory(dir, buildServerPath);
        }

		return true;
	}

    public void PushPackagesInDirectory(string dir, string buildServerPath)
    {
        var pkgFile = GetNewestFile(dir);

        pkgFile = ToRelative(pkgFile);

        Console.WriteLine("Package file:");
        Console.WriteLine(" " + pkgFile);

        var arguments = "push"
            + " " + pkgFile
            + " -Source " + buildServerPath;

        ExecuteScript("nuget", arguments);
    }

    public string GetBuildServerPath()
    {
        var path = BaseNode.Properties["PackageServerPath"];

        path = ToAbsolute(
            Path.GetDirectoryName(BaseNode.FilePath),
            path
        );

        return path;
    }
}
