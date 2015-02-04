//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.IO.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";

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

            var libDir = CurrentDirectory
                + Path.DirectorySeparatorChar
                + "lib";

            Relocate(tmpDir);

            StartDotNetExe(
                "lib/nuget.exe",
                "install",
                "csAnt",
                "-Source " + libDir,
                "-Source " + pkgDir,
                "-Source " + "https://www.myget.org/F/softwaremonkeys/",
                "-Source " + "https://go.microsoft.com/fwlink/?LinkID=206669",
                "-OutputDirectory lib",
                "-NoCache"
            );

            Relocate(OriginalDirectory);

            new FileCopier(
                Path.Combine(tmpDir, "lib"),
                Path.Combine(CurrentDirectory, "pkg")
            ).Copy(
                "**.nupkg",
                "!csAnt" // Don't copy the csAnt packages back because they're already there
            );

            return !IsError;
	}
}
