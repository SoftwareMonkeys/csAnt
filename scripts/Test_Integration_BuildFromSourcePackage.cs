//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_BuildFromSourcePackageScript : BaseProjectTestScript
{
    public string SourceProjectDirectory;
    public string TestProjectDirectory;

	public static void Main(string[] args)
	{
		new Test_BuildFromSourcePackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Testing a build cycle from the source package...");
		Console.WriteLine("");

        SourceProjectDirectory = CurrentDirectory;
		
        new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalFiles();

        CreateNodes();

		ExecuteScript("EnsurePackage", "csAnt-src");

		if (!IsError)
		{
			DeployAndBuild();
		}
		
		return !IsError;
	}

	public void DeployAndBuild()
	{
        TestProjectDirectory = GetTmpDir();

		var releaseZipFile = GetNewestFile(
			SourceProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "pkg"
			+ Path.DirectorySeparatorChar
			+ "csAnt-src"
		);

		Unzip(releaseZipFile, TestProjectDirectory);			

		Console.WriteLine("");
		Console.WriteLine("Test project directory:");
		Console.WriteLine(" " + TestProjectDirectory);
		Console.WriteLine("");

		Relocate(TestProjectDirectory);

		if (!IsError)
		{
			Console.WriteLine("");
			Console.WriteLine("Building...");
			Console.WriteLine("");

			ExecuteScript("CycleBuild");
		}
	}
}
