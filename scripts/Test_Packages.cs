//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.Packages;

class Test_PackagesIntegration : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_PackagesIntegration().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        FilesGrabber.GrabOriginalFiles();

                var packageName = "TestPackage";
	        
                // Create a package
                ExecuteScript("CreatePackage", packageName);

                // Add files to package
                ExecuteScript("AddFilesToPackage", packageName, "scripts/*.cs");

                // Build the package
                ExecuteScript("BuildPackage", packageName);

                var repoPath = GetTmpDir()
                    + Path.DirectorySeparatorChar
                    + "pkgs";
                
                // Send the package to the repository
                ExecuteScript("SendPackage", packageName, repoPath);

		return !IsError;
	}
}
