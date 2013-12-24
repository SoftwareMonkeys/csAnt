//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.PackageManager.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.PackageManager;

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

		if (args.Length < 2)
		{
                    Help();
		}
                else
                {
                    var name = args[0];

                    var pattern = args[1];

                    Console.WriteLine("Name: " + name);
                    Console.WriteLine("Pattern:" + pattern);
                    Console.WriteLine(name);
                    Console.WriteLine("");

                    Packages.AddFile(name, pattern);
                }

		return !IsError;
	}

	public void Help()
        {
            Console.WriteLine("Please provide two arguments; the name of the package and the file/pattern.");
        }
}
