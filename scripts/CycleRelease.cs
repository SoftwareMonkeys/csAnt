//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class CycleReleaseScript : BaseScript
{
	public static void Main(string[] args)
	{
		new CycleReleaseScript().Start();
	}
	
	public void Start()
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full release cycle.");
		Console.WriteLine("");

		ExecuteScript("CycleBuild");

		if (!IsError)
		{
			Console.WriteLine("Creating release zip files...");
			Console.WriteLine("");

			// Create the release
			ExecuteScript(
				"Release",
				new string[]{
					"-mode:Release"
				}
			);
		}

		if (!IsError)
		{
			Console.WriteLine("Uploading the release zip file to GoogleCode...");
			Console.WriteLine("");

			// Upload to GoogleCode
			ExecuteScript("GoogleCodeRelease");
		}
	}
}
