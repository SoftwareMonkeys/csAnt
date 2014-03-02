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
		Console.WriteLine("");
		Console.WriteLine("Ensuring release has been created...");
		Console.WriteLine("");

		var releaseName = "";
		if (args.Length > 0)
		{
			releaseName = args[0];
			Console.WriteLine("Release name: " + releaseName);
		}

		var currentVersion = CurrentNode.Properties["Version"];

		Console.WriteLine("Current version: " + currentVersion);

		CheckReleaseVersions(releaseName, currentVersion);

		return !IsError;
	}

	public void CheckReleaseVersions(string releaseName, string currentVersion)
	{
		var rlsDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "rls";

		var releaseDirectoryCount = Directory.GetDirectories(rlsDir).Length;

		var releaseListCount = Directory.GetFiles(rlsDir, "*.cs").Length;

		// If any release zip directories are missing then the release cycle is started
		if (releaseListCount > releaseDirectoryCount)
		{
			Console.WriteLine("There are more release lists than release directories, which means some release zip files must need to be created. Running release cycle (CycleRelease script) now...");

			Console.WriteLine("");

			// TODO: Perform release cycle only on missing releases instead of all of them
			if (!String.IsNullOrEmpty(releaseName))
				ExecuteScript("CycleRelease", releaseName);
			else
				ExecuteScript("CycleRelease");
		}
		else
		{
			// Loop through each release zip directory and check whether it's up to date
			foreach (var dir in Directory.GetDirectories(rlsDir))
			{
				var dirName = Path.GetFileName(dir);

				if (String.IsNullOrEmpty(releaseName)
					|| dirName.ToLower() == releaseName.ToLower())
				{
					var version = GetReleaseVersion(dir);

					Console.WriteLine(version);

					releaseName = Path.GetFileName(dir);

					Console.WriteLine("");
					Console.WriteLine("Release: " + releaseName);
					Console.WriteLine("Release version: " + version);
					Console.WriteLine("Current version: " + currentVersion);

					if (new Version(version) < new Version(currentVersion))
					{
						Console.WriteLine("Current version is later than the latest release. Running release script again...");

						if (!String.IsNullOrEmpty(releaseName))
							ExecuteScript("CycleRelease", releaseName);
						else
							ExecuteScript("CycleRelease");
					}
					else
					{
						Console.WriteLine("Release version matches current version. Skipping release generation.");
					}

					Console.WriteLine("");
				}
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
		
		if (IsVerbose)
			Console.WriteLine("Prefix: " + prefix);

		var withoutPrefix = latestFileName.Replace(prefix, "");
		
		if (IsVerbose)
			Console.WriteLine("Without prefix: " + withoutPrefix);
		

		var versionStartPos = 0;

		var versionEndPos = withoutPrefix.IndexOf("[")-1;

		var withoutTimestamp = withoutPrefix.Substring(versionStartPos, versionEndPos);

		if (IsVerbose)
			Console.WriteLine("Without time stamp: " + withoutTimestamp);

		var version = withoutTimestamp.Replace("-", ".");

		return version;
	}
}
