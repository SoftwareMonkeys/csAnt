//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.Contracts.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Packages;
using SoftwareMonkeys.csAnt.Projects;

class CreatePackageScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CreatePackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Creating new package...");
		Console.WriteLine("");

		var name = args[0];

                Console.WriteLine("Name: " + name);
                Console.WriteLine("");

                var groupName = GroupNode.Name;

                if (args.Length > 1)
                    groupName = args[1];

                Console.WriteLine("Group name: " + groupName);
                Console.WriteLine("");

                var packages = new PackageManager(CurrentDirectory);

		packages.Create(name, groupName);

		return !IsError;
	}
}
