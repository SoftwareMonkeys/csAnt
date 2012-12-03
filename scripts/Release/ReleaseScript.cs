//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;

class ReleaseScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ReleaseScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		var listDir = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "rls";

		// Loop through the folder containing release list files
		foreach (string listFile in Directory.GetFiles(listDir, "*-list.txt"))
		{
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

				var dateStamp = "["
					+ DateTime.Now.Year
					+  "-"
					+ DateTime.Now.Month
					+ "-"
					+ DateTime.Now.Day
					+ "--"
					+ DateTime.Now.Hour
					+ "-"
					+ DateTime.Now.Minute
					+ "-"
					+ DateTime.Now.Second
					+ "]";

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
			} 
			else
				Console.WriteLine("No files or patterns specified in the release file list.");

			Console.WriteLine("");
		}

		return !IsError;
	}
}
