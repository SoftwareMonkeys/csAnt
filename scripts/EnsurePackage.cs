//css_ref ../lib/csAnt/bin/Package/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Package/SoftwareMonkeys.csAnt.Projects.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class EnsurePackageScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new EnsurePackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Ensuring packages have been created...");
		Console.WriteLine("");

		var packageName = "";
		if (args.Length > 0)
		{
			packageName = args[0];
			Console.WriteLine("Package name: " + packageName);
		}

		var currentVersion = CurrentNode.Properties["Version"];

		Console.WriteLine("Current version: " + currentVersion);

		CheckPackageVersions(packageName, currentVersion);

		return !IsError;
	}

	public void CheckPackageVersions(string packageName, string currentVersion)
	{
		// TODO: Completely reorganize this function and break it into smaller functions

		var pkgDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "pkg";

		var packageDirectoryCount = Directory.GetDirectories(pkgDir).Length;

		var packageListCount = Directory.GetFiles(pkgDir, "*.nuspec").Length;

		// If any package zip directories are missing then the package cycle is started
		if (packageListCount > packageDirectoryCount)
		{
			Console.WriteLine("There are more package lists than package directories, which means some package zip files must need to be created. Running package cycle (CyclePackage script) now...");

			Console.WriteLine("");

			// TODO: Perform package cycle only on missing packages instead of all of them
			if (!String.IsNullOrEmpty(packageName))
				ExecuteScript("CyclePackage", packageName);
			else
				ExecuteScript("CyclePackage");
		}
		else
		{
			// Check whether the packages are up to date
			foreach (var listFile in Directory.GetFiles(pkgDir, "*.nuspec"))
			{
				var name = Path.GetFileNameWithoutExtension(listFile);

				var dir = pkgDir
					+ Path.DirectorySeparatorChar
					+ name;

				bool needsPackage = false;

				if (Directory.Exists(dir))
				{
					if (String.IsNullOrEmpty(packageName)
						|| name.ToLower() == packageName.ToLower())
					{
						var version = GetPackageVersion(dir);

						Console.WriteLine(version);

						packageName = Path.GetFileName(dir);

						Console.WriteLine("");
						Console.WriteLine("Package: " + packageName);
						Console.WriteLine("Package version: " + version);
						Console.WriteLine("Current version: " + currentVersion);

						if (new Version(version) < new Version(currentVersion))
						{
							Console.WriteLine("Current version is later than the latest package. Running package script again...");

							needsPackage = true;
						}
						else
						{
							Console.WriteLine("Package version matches current version. Skipping package generation.");
						}

						Console.WriteLine("");
					}
				}
				else
					needsPackage = true;

				if (needsPackage)
				{
					if (!String.IsNullOrEmpty(packageName))
						ExecuteScript("CyclePackage", packageName);
					else
						ExecuteScript("CyclePackage");
				}
			}
		}
	}

	public string GetPackageVersion(string packageSubDir)
	{
		var latestFilePath = GetNewestFile(packageSubDir);

		var latestFileName = Path.GetFileNameWithoutExtension(latestFilePath);

		Console.WriteLine("Package file: " + latestFileName);

		var variation = Path.GetFileName(packageSubDir);

		var prefix = ProjectName + "-" + variation + ".";
		
		if (IsVerbose)
			Console.WriteLine("Prefix: " + prefix);

		var withoutPrefix = latestFileName.Replace(prefix, "");
		
		if (IsVerbose)
			Console.WriteLine("Without prefix: " + withoutPrefix);
		

		var versionStartPos = latestFileName.IndexOf(".")+1;

		var versionEndPos = latestFileName.Length-versionStartPos-1;

		var withoutTimestamp = withoutPrefix.Substring(versionStartPos, versionEndPos);

		if (IsVerbose)
			Console.WriteLine("Without time stamp: " + withoutTimestamp);

		var version = withoutTimestamp.Replace("-", ".");

		return version;
	}
}
