//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;

class CloneGitScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CloneGitScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
	//	var parser = new Arguments(args);

		var dir = GetTemporaryDirectory();

		EnsureDirectoryExists(dir);

		if (args.Length > 0)
			dir = Path.GetFullPath(args[0]);

		CloneGit(ProjectDirectory, dir);

		return !IsError;
	}

	public void CloneGit(
		string sourceDir,
		string destinationDir
	)
	{
		/*var gitExe = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "lib"
			+ Path.DirectorySeparatorChar
			+ "GitSharp"
			+ Path.DirectorySeparatorChar
			+ "Git.exe";
*/
		Console.WriteLine ("Cloning project via git...");

		Console.WriteLine ("");
		Console.WriteLine ("Source:");
		Console.WriteLine (sourceDir);

		Console.WriteLine ("");
		Console.WriteLine ("Destination:");
		Console.WriteLine (destinationDir);
		
		Console.WriteLine ("");

/*		var parameters = new string[]{
			sourceDir
		};
*/
		CurrentDirectory = destinationDir;

		Console.WriteLine (Environment.CurrentDirectory);

		/*StartProcess(
			"mono",
			parameters
		);*/
	}
}
