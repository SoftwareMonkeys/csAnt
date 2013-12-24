//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.Contracts.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Packages;

class SendPackageScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new SendPackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
                var packageName = args[0];

                var groupName = args[1];

                var repoPath = args[2];

		Console.WriteLine("Package name: " + packageName);
		Console.WriteLine("Group name: " + groupName);
		Console.WriteLine("Repository path: ");
		Console.WriteLine(repoPath);

		if (!IsAbsolute(repoPath))
			repoPath = ToAbsolute(repoPath);
	
                var packages = new PackageManager(CurrentDirectory);
                packages.Send(packageName, groupName, repoPath);

		return !IsError;
	}
}
