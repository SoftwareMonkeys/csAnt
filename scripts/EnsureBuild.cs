//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt;
using System.Linq;

class EnsureBuildScript : BaseScript
{
	public static void Main(string[] args)
	{
		new EnsureBuildScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var timeStampsData = GetTimeStampsData();

		var latestTimeStamps = GetLatestTimeStamps();

		var needsBuild = false;

		// If there's a different number of 
		if (timeStampsData.Count != latestTimeStamps.Count)
		{
			Console.WriteLine("Different number of files detected... needs rebuild...");
			Console.WriteLine("Previous number: " + timeStampsData.Count);
			Console.WriteLine("New number: " + latestTimeStamps.Count);

			needsBuild = true;
		}
		else
		{
			foreach (KeyValuePair<string, string> entry in timeStampsData)
			{
				var key = entry.Key;

				/*var key2 = latestTimeStamps.Keys.ElementAt(i);

				if (key1 != key2)
				{
					Console.WriteLine("Files don't match (likely one was added or removed:");
					Console.WriteLine("File 1: " + key1);
					Console.WriteLine("File 2: " + key2);

					needsBuild = true;
					break;
				}*/

				var timeStamp1 = timeStampsData[key];

				var timeStamp2 = latestTimeStamps[key];

				if (timeStamp1 != timeStamp2)
				{
					Console.WriteLine("File has changed:");
					Console.WriteLine(key);

					Console.WriteLine("");
					Console.WriteLine("Launching build cycle (CycleBuild script)");

					needsBuild = true;
					break;
				}
			}
		}

		if (needsBuild)
		{
			ExecuteScript("CycleBuild");

			WriteTimeStampsData(latestTimeStamps);
		}
		else
			Console.WriteLine("All builds are up to date. Skipping build.");

		return !IsError;
	}

	public Dictionary<string, string> GetTimeStampsData()
	{
		var timeStampsFile = GetTimeStampsFile();

		var timeStamps = new Dictionary<string, string>();

		if (File.Exists(timeStampsFile))
		{
			foreach (var line in File.ReadAllLines(timeStampsFile))
			{
				if (!String.IsNullOrEmpty(line))
				{
					var parts = line.Split('|');

					var fileName = parts[0];

					var timeStamp = parts[1];

					timeStamps.Add(fileName, timeStamp);
				}
				
			}
		}

		return timeStamps;
	}

	public Dictionary<string, string> GetLatestTimeStamps()
	{
		var patterns = GetPatterns();

		var srcDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "src";

		var files = FindFiles(srcDir, patterns);

		var timeStamps = new Dictionary<string, string>();

		foreach (var file in files)
		{
			timeStamps.Add(
				file.Replace(CurrentDirectory, ""),
				File.GetLastWriteTime(file).ToString()
			);
		}

		return timeStamps;
	}

	public string[] GetPatterns()
	{
		return new string[]{
			"**.cs",
			"**.csproj",
			"**.sln"
		};
	}

	public void WriteTimeStampsData(Dictionary<string, string> timeStampsData)
	{
		var timeStampsFile = GetTimeStampsFile();

		List<string> lines = new List<string>();

		foreach (var key in timeStampsData.Keys)
		{
			var timeStamp = timeStampsData[key];

			lines.Add(key + "|" + timeStamp);
		}

		File.WriteAllLines(timeStampsFile, lines.ToArray());
	}

	public string GetTimeStampsFile()
	{
		return CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "src"
			+ Path.DirectorySeparatorChar
			+ "TimeStamps.txt";
	}
}