//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.Contracts.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Packages;

class BuildPackageScript : BaseScript
{
	public static void Main(string[] args)
	{
		new BuildPackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Building package...");
		Console.WriteLine("");

		if (args.Length < 1)
		{
                    Help();
		}
                else
                {
                    var name = args[0];

                    Console.WriteLine("Name: " + name);
                    Console.WriteLine("");

                    var groupName = args[1];

                    Console.WriteLine("Group name: " + groupName);
                    Console.WriteLine("");

                    var packages = new PackageManager(CurrentDirectory);
                    
                    packages.Build(name, groupName);

                    RaiseEvent("BuildPackage");
                }

		return !IsError;
	}

	public void Help()
        {
            Console.WriteLine("Please provide the name of the package to build as an argument.");
        }
}
