//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.Contracts.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Packages;

class CreatePackageScript : BaseScript
{
	public static void Main(string[] args)
	{
		new CreatePackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
                if (Arguments.Contains("h")
                    || Arguments.Contains("help"))
                {
                    Help();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Creating new package repository...");
                    Console.WriteLine("");

                    var name = args[0];

                    Console.WriteLine("Name: " + name);
                    Console.WriteLine("");

                    var path = ToAbsolute(
                        OriginalDirectory
                        + "../../../pkgs"
                    );

                    if (args.Length > 1)
                    {
                        path = ToAbsolute(args[1]);
                    }

                    Console.WriteLine("Path: " + path);
                    Console.WriteLine("");

                    var packages = new PackageManager(CurrentDirectory);

                    packages.Repositories.Create(name, path);
		}

		return !IsError;
	}

	public void Help()
	{
            Console.WriteLine("");
            Console.WriteLine("Help");
            Console.WriteLine("");
            Console.WriteLine("Two arguments are expected; repository name, and repository path.");
	}
}
