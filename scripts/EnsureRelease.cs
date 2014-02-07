//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class EnsureReleaseScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new EnsureReleaseScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var currentVersion = CurrentNode.Properties["Version"];

		Console.WriteLine("Current version: " + currentVersion);

		CheckReleaseVersions(currentVersion);

		return !IsError;
	}

	public void CheckReleaseVersions(string currentVersion)
	{
		var rlsDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "rls";

		var releaseDirectoryCount = Directory.GetDirectories(rlsDir).Length;

		var releaseListCount = Directory.GetFiles(rlsDir).Length;

		// If any release zip directories are missing then the release cycle is started
		if (releaseListCount > releaseDirectoryCount)
		{
			Console.WriteLine("There are more release lists than release directories, which means some release zip files must need to be created. Running release cycle (CycleRelease script) now...");

			Console.WriteLine("");

			// TODO: Perform release cycle only on missing releases instead of all of them
			ExecuteScript("CycleRelease");
		}
		else
		{
			// Loop through each release zip directory and check whether it's up to date
			foreach (var dir in Directory.GetDirectories(rlsDir))
			{
				var version = GetReleaseVersion(dir);

				Console.WriteLine(version);

				var variation = Path.GetFileName(dir);

				Console.WriteLine("");
				Console.WriteLine("Release: " + variation);
				Console.WriteLine("Release version: " + version);
				Console.WriteLine("Current version: " + currentVersion);

				if (new Version(version) < new Version(currentVersion))
				{
					Console.WriteLine("Current version is later than the latest release. Running release script again...");

					ExecuteScript("CycleRelease", variation);
				}

				Console.WriteLine("");
			}
		}
	}

	public string GetReleaseVersion(string releaseSubDir)
	{
		var latestFilePath = GetNewestFile(releaseSubDir);

		var latestFileName = Path.GetFileNameWithoutExtension(latestFilePath);

		Console.WriteLine("Release file: " + latestFileName);

		var variation = Path.GetFileName(releaseSubDir);

		var prefix = ProjectName + "-" + variation + "-";

		Console.WriteLine("Prefix: " + prefix);

		var startPos = 0;

		var endPos = latestFileName.IndexOf("[")-2;

		var withoutPrefix = latestFileName.Replace(prefix, "");

		Console.WriteLine("Without prefix: " + withoutPrefix);

		var withoutTimestamp = withoutPrefix.Substring(startPos, endPos);

		Console.WriteLine("Without time stamp: " + withoutPrefix);

		var version = withoutTimestamp.Replace("-", ".");

		return version;
	}
}
