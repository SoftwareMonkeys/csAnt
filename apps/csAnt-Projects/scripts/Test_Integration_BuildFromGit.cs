//css_ref "SoftwareMonkeys.csAnt.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.Scripting.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll";

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_BuildFromGitScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_BuildFromGitScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Test building solutions from git clone...");
		Console.WriteLine("");

        // Clone to test dir
        Console.WriteLine("Cloning to test directory...");

        Git.Clone(OriginalDirectory, CurrentDirectory);

        Nodes.EnsureNodes();
        Nodes.Refresh();

        // Grab the required library files
        Console.WriteLine("Grabbing required library files...");

        new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalLibFiles();

        // Launch the build cycle
        Console.WriteLine("Launching build cycle...");

        ExecuteScript("CycleBuild");

		return !IsError;
	}

}
