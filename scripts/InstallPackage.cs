//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Packages;

class InstallPackageScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new InstallPackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
                if (IsHelp)
                    Help();
                else
                {
                    var repositoryPath = "";

                    if (Arguments.Contains("r"))
                        repositoryPath = Arguments["r"];

                    var packageName = "";

                    if (Arguments.Contains("p"))
                        packageName = Arguments["p"];
                        
                    var groupName = "";

                    if (Arguments.Contains("g"))
                        groupName = Arguments["g"];


                    Console.WriteLine("Package name: " + packageName);
                    Console.WriteLine("Group name: " + groupName);
                    Console.WriteLine("Repository path: ");
                    Console.WriteLine(repositoryPath);

                    var packages = new PackageManager(CurrentDirectory);

                    packages.Install(packageName, groupName, repositoryPath);

                    
                }
		return !IsError;
	}

	public void Help()
	{
            Console.WriteLine("");
            Console.WriteLine("Help");
            Console.WriteLine("");
            Console.WriteLine("Arguments:");
            Console.WriteLine(" -r: Repository path");
            Console.WriteLine(" -p: Package name");
            Console.WriteLine(" -g: Group/company name");
            Console.WriteLine("");
	}
}
