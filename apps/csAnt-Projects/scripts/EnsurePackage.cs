//css_ref ../lib/csAnt/bin/Package/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Package/SoftwareMonkeys.csAnt.Projects.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class EnsurePackageScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new EnsurePackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Ensuring packages have been created...");
		Console.WriteLine("");

		var packageName = "";
		if (Arguments.KeylessArguments.Length > 0)
		{
			packageName = Arguments.KeylessArguments[0];
			Console.WriteLine("Package name: " + packageName);
		}

		var currentVersion = CurrentNode.Properties["Version"];

		Console.WriteLine("Current version: " + currentVersion);

		var checker = new PackageChecker(new Version(currentVersion));

		if (!String.IsNullOrEmpty(packageName))
		    checker.Check(packageName);
		else
		    checker.Check();

		return !IsError;
	}

}
