using System;
using System.IO;
using SoftwareMonkeys.csAnt;

class FixNUnitRunners : BaseScript
{
	public static void Main(string[] args)
	{
		new FixNUnitRunners().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        Console.WriteLine("");
        Console.WriteLine("Fixing NUnit.Runners library so it can be used...");
        Console.WriteLine("");

        var nunitToolsDir = CurrentDirectory
            + Path.DirectorySeparatorChar
            + "lib"
            + Path.DirectorySeparatorChar
            + "NUnit.Runners.2.6.0.12051"
            + Path.DirectorySeparatorChar
            + "tools";

        var fromDir = nunitToolsDir
            + Path.DirectorySeparatorChar
            + "lib";

        var toDir = nunitToolsDir;

        Console.WriteLine("Copying files:");
        Console.WriteLine("");

        foreach (var file in Directory.GetFiles(fromDir))
        {
            var toFile = file.Replace(fromDir, toDir);

            Console.WriteLine(file.Replace(fromDir, ""));

            File.Copy(file, toFile, true);
        }

		return !IsError;
	}
}
