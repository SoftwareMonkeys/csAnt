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
	
	public override bool Start(string[] args)
	{
		if (args.Length > 0 && !args[0].StartsWith("-"))
		{
			string releaseList = args[0];

			Console.WriteLine("Release list: " + releaseList);
			
			CreateRelease(releaseList);
		}
		else
		{
			Console.WriteLine("No release list specified. Generating all releases.");

			var listDir = GetReleaseDir();

			// Loop through the folder containing release list files
			foreach (string listFile in Directory.GetFiles(listDir, "*-list.txt"))
			{
				CreateRelease(listFile);

			}
		}

		return !IsError;
	}

	public void CreateRelease(string listFile)
	{
		if (listFile.IndexOf("-list.txt") == -1)
		{
			listFile = GetReleaseDir()
				+ listFile
				+ "-list.txt";
		}

		Console.WriteLine("----------------------------------------------------------------------");
		Console.WriteLine("Release list file: " + listFile.Replace(ProjectDirectory, ""));

		var files = new List<string>(File.ReadLines(listFile)).ToArray();		

		if (files.Length > 0)
		{
			Console.WriteLine(" ");
			Console.WriteLine("Patterns:");

			foreach (string file in files)
			{
				Console.WriteLine("  " + file);
			}

			Console.WriteLine(" ");

			var variation = Path.GetFileNameWithoutExtension(listFile).Replace("-list", "");

			var dateStamp = "[" + TimeStamp + "]";

			var zipFileName = ProjectName
				+ "-"
				+ variation
				+ "-"
				+ dateStamp
				+ ".zip";

			var zipFilePath = ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "rls"
				+ Path.DirectorySeparatorChar
				+ variation
				+ Path.DirectorySeparatorChar
				+ zipFileName;

			if (!Directory.Exists(Path.GetDirectoryName(zipFilePath)))
				Directory.CreateDirectory(Path.GetDirectoryName(zipFilePath));

			Console.WriteLine("Zip file path: " + zipFilePath);

			Zip(
				files,
				zipFilePath
			);

			Console.WriteLine("  Release file: " + zipFilePath.Replace(ProjectDirectory, ""));
			Console.WriteLine("Release zip file created successfully.");
			Console.WriteLine("");
			Console.WriteLine("----------------------------------------------------------------------");

			AddSummary("Generated '" + zipFileName + "' release file from '" + Path.GetFileName(listFile) + "' list file.");

			ExportToGeneralLibs(zipFilePath);
		} 
		else
			Console.WriteLine("No files or patterns specified in the release file list.");

		Console.WriteLine("");
	}

	public void ExportToGeneralLibs(string zipFilePath)
	{
		var generalLibsDir = Path.GetFullPath(
			ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ ".."
			+ Path.DirectorySeparatorChar
			+ "lib"
		);

		var generalProjectLibsDir = generalLibsDir
			+ Path.DirectorySeparatorChar
			+ ProjectName;

		var toFile = generalProjectLibsDir
			+ Path.DirectorySeparatorChar
			+ Path.GetFileName(zipFilePath);

		EnsureDirectoryExists(generalProjectLibsDir);

		Console.WriteLine("Exporting release file:");
		Console.WriteLine(zipFilePath);
		Console.WriteLine("To:");
		Console.WriteLine(toFile);

		AddSummary("Exported release to: " + Path.GetDirectoryName(toFile));

		File.Copy(zipFilePath, toFile, true);

		// Create a copy without the timestamp (so its easy to know the path to the latest release)

		//var shortFileName = Path.GetFileNameWithoutExtension(toFile);

		//var pos1 = shortFileName.IndexOf("[")-1;

		//var pos2 = shortFileName.IndexOf("]")+1;

		var fileTimeStamp = "-[" + TimeStamp + "]";//shortFileName.Substring(pos1, pos2-pos1);

		Console.WriteLine(fileTimeStamp);
		var modifiedToFile = toFile.Replace(fileTimeStamp, "");

		Console.WriteLine("");
		Console.WriteLine("Modified file name:");
		Console.WriteLine(modifiedToFile);
		Console.WriteLine("");

		File.Copy(toFile, modifiedToFile, true);
	}

	public string GetReleaseDir()
	{
		return ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "rls"
			+ Path.DirectorySeparatorChar;
	}
}
