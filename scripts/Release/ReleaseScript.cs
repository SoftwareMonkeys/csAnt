//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;

/// <summary>
/// Generates release zip files based on the '/rls/*-list.txt' files and places them into corresponding '/rls/*/' directories.
/// </summary>
class ReleaseScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ReleaseScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		if (args.Length > 0 && !args[0].StartsWith("-"))
		{
			string releaseList = args[0];

			Console.WriteLine("Release list: " + releaseList);
			
			Release(releaseList);
		}
		else
		{
			Console.WriteLine("No release list specified. Generating all releases.");

			Release();
		}

		return !IsError;
	}

}
