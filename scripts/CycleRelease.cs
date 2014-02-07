//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CycleReleaseScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CycleReleaseScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full release cycle.");
		Console.WriteLine("");

		var releaseName = String.Empty;

		if (args.Length > 0){
			releaseName = args[0];
			Console.WriteLine("Release name: " + releaseName);
		}

		ExecuteScript("EnsureBuild");
		
		ExecuteScript("ClearBak");

		if (!IsError)
		{
			Console.WriteLine("Creating release zip files...");
			Console.WriteLine("");

			List<string> execArgs = new List<string>();

			if (releaseName != String.Empty)
				execArgs.Add(releaseName);

			// Create the release
			ExecuteScript(
				"Release",
				execArgs.ToArray()
			);
		}

		return !IsError;
	}
}
