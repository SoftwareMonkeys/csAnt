//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.Contracts.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Packages;

class AddFilesToPackageScript : BaseScript
{
	public static void Main(string[] args)
	{
		new AddFilesToPackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Adding files to package...");
		Console.WriteLine("");

		if (args.Length < 3)
		{
                    Help();
		}
                else
                {
                    var name = args[0];
                    
                    var groupName = args[1];

                    var pattern = args[2];

                    Console.WriteLine("Name: " + name);
                    Console.WriteLine("Group name:" + groupName);
                    Console.WriteLine("Pattern:" + pattern);
                    Console.WriteLine("");

                    var packages = new PackageManager(CurrentDirectory);
                    packages.AddFile(name, groupName, pattern);
                }

		return !IsError;
	}

	public void Help()
        {
            Console.WriteLine("Please provide two arguments; the name of the package, the group, and the file/pattern.");
        }
}
