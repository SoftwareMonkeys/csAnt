//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Packages.dll;
//css_ref ../lib/NUnit/bin/framework/nunit.framework.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.Packages;
using SoftwareMonkeys.csAnt.IO;
using NUnit.Framework;

class Test_PackagesIntegration : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_PackagesIntegration().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        new FilesGrabber(
                    OriginalDirectory,
                    CurrentDirectory
                ).GrabOriginalScriptingFiles();

                var packageName = "TestPackage";

                var groupName = "TestOrganization";

                // Create a package
                ExecuteScript("CreatePackage", packageName, groupName);

                CreateInstallScript(packageName, groupName);

                // Add files to package
                ExecuteScript("AddFilesToPackage", packageName, groupName, "scripts/*.cs");

                var repoPath = GetTmpDir()
                    + Path.DirectorySeparatorChar
                    + "pkgs";

                // Send the package to the repository
                ExecuteScript("CreatePackageRepository", "local", repoPath);
                
                // Build the package
                ExecuteScript("BuildPackage", packageName, groupName);

                // Send the package to the repository
                ExecuteScript("SendPackage", packageName, groupName, repoPath);

                ExecuteScript(
                    "InstallPackage",
                    "-p:" + packageName,
                    "-g:" + groupName,
                    "-r:" + ToRelative(repoPath)
                );

                Assert.IsTrue(Console.Out.ToString().Contains("Executed package install script."), "The install script didn't execute.");
                
		return !IsError;
	}

	public void CreateInstallScript(string packageName, string groupName)
	{
            var content = @"//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Contracts.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

public class InstallScript : BaseScript
{
        public static void Main(string[] args)
        {
                new InstallScript().Start(args);
        }

        public override bool Run(string[] args)
        {
                Console.WriteLine("""");
                Console.WriteLine(""Installed the test package."");
                Console.WriteLine(""Executed package install script."");
                Console.WriteLine("");

                AddSummary(""Installed the test package."");

                return !IsError;
        }
}
";

            var filePath = CurrentDirectory
                + Path.DirectorySeparatorChar
                + "pkgs"
                + Path.DirectorySeparatorChar
                + groupName
                + Path.DirectorySeparatorChar
                + packageName
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + "InstallPackage.cs";

             EnsureDirectoryExists(Path.GetDirectoryName(filePath));

             Console.WriteLine("Creating fake install script:");
             Console.WriteLine(filePath);

             File.WriteAllText(filePath, content);
            
        }
}
