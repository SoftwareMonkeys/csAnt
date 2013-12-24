//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.PackageManager.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.PackageManager;

class CreatePackageScript : BaseScript
{
	public static void Main(string[] args)
	{
		new CreatePackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Creating new package repository...");
		Console.WriteLine("");

                var name = args[0];

                Console.WriteLine("Name: " + name);
                Console.WriteLine("");

                var path = Path.GetFullPath(
                    OriginalDirectory
                    + "../../../pkgs"
                );

                if (args.Length > 1)
                    path = Path.GetFullPath(args[1]);

                Console.WriteLine("Path: " + path);
                Console.WriteLine("");

		Packages.Repositories.Create(name, path);

		return !IsError;
	}
}
