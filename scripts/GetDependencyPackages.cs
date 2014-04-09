//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.IO.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;

class GetDependencyPackages : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new GetDependencyPackages().Start(args);
	}
	
	public override bool Run(string[] args)
	{
            Console.WriteLine("");
            Console.WriteLine("Getting dependency packages as specified in /pkg/*.nuspec files...");
            Console.WriteLine("");

            var tmpDir = GetTmpDir();

            new FileCopier(
                CurrentDirectory,
                tmpDir
            ).Copy(
                "lib/nuget.exe"
            );

            var pkgDir = CurrentDirectory
                + Path.DirectorySeparatorChar
                + "pkg";

            Relocate(tmpDir);

            StartDotNetExe(
                "lib/nuget.exe",
                "install",
                "csAnt",
                "-Source " + pkgDir,
                "-Source " + "https://www.myget.org/F/csant/",
                "-Source " + "https://go.microsoft.com/fwlink/?LinkID=206669",
                "-OutputDirectory lib",
                "-NoCache"
            );

            Relocate(OriginalDirectory);

            new FileCopier(
                Path.Combine(tmpDir, "lib"),
                Path.Combine(CurrentDirectory, "pkg")
            ).Copy(
                "pkg/**.nupkg",
                "!pkg/csAnt" // Don't copy the csAnt packages back because they're already there
            );

            return !IsError;
	}
}
