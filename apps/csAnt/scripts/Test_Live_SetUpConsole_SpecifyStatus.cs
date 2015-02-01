//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;
//css_ref ../lib/NUnit.2.6.0.12051/lib/nunit.framework.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting.Live;
using NUnit.Framework;

class Test_Live_SetUpConsole_SpecifyStatus : BaseLiveProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_Live_SetUpConsole_SpecifyStatus().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Testing setup (live)...");
		Console.WriteLine("");

        var status = "beta";

		var setUpExeFile = OriginalDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		var setUpExeToFile = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "csAnt-SetUp.exe";

		File.Copy(setUpExeFile, setUpExeToFile, true);

		StartDotNetExe(setUpExeToFile, "-status=" + status);

		if (IsLinux)
			StartProcess("sh csAnt.sh HelloWorld");
		else
			StartProcess("csAnt.bat HelloWorld"); 

        var libDir = Path.Combine(CurrentDirectory, "lib");

        var csAntLibDir = Directory.GetDirectories(libDir, "csAnt.*")[0];

        Assert.IsTrue(
            csAntLibDir.Trim(Path.DirectorySeparatorChar).EndsWith(status),
            "Package doesn't contain the status."
        );

		return !IsError;
	}
}
