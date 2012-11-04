//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;

class UpdateScript : BaseScript
{
	public static void Main(string[] args)
	{
		new UpdateScript().Start();
	}
	
	public void Start()
	{
		GetRemotecsAnt();

		Console.WriteLine ("Update complete.");
	}
	
	public void GetRemotecsAnt()
	{
		var remotePath = GetRemotecsAntFilePath();

		Console.WriteLine("Remote csAnt path: " + remotePath);

		var projectDirectory = ProjectDirectory;

		var tmpDir = projectDirectory
			+ Path.DirectorySeparatorChar
			+ "_tmp_update";

		Console.WriteLine("To: " + tmpDir);

		DownloadAndUnzip(
			remotePath,
			tmpDir
		);

		// Move from the tmp directory to the destination
		MoveToDestination(
			tmpDir
		);
	}
	
	public void MoveToDestination(string tmpDir)
	{
		string subDir = Directory.GetDirectories(tmpDir)[0];

		var baseDir = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "_update";

		if (!Directory.Exists(baseDir))
			Directory.CreateDirectory(baseDir);

		Console.WriteLine ("Updating files:");

		foreach (string file in Directory.GetFiles (subDir, "*", SearchOption.AllDirectories))
		{
			var toFile = file.Replace(subDir, baseDir);

			if (!Directory.Exists(Path.GetDirectoryName(toFile)))
				Directory.CreateDirectory(Path.GetDirectoryName(toFile));


			if (
				// If the file is newer than the existing one
				File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile)
			)
			{
				if (
					!File.Exists(toFile)
				    // If both files are different
					|| !FileEquals(file, toFile)
				)
				{
					Console.WriteLine ("Updating:");
					Console.WriteLine ("  " + toFile.Replace(ProjectDirectory, ""));

					if (File.Exists(toFile))
					{
						BackupFile(toFile.Replace(baseDir, "").Trim(Path.DirectorySeparatorChar));

						File.Delete(toFile);
					}

					File.Move(file, toFile);
				}
				else
				{
					Console.WriteLine ("Skipping (file hasn't changed):");
					Console.WriteLine ("  " + toFile.Replace(ProjectDirectory, ""));
				}
			}
			else
			{
				Console.WriteLine ("Skipping (file isn't newer):");
				Console.WriteLine ("  " + toFile.Replace(ProjectDirectory, ""));
			}
		}

		Directory.Delete(tmpDir, true);
	}

	public string GetRemotecsAntFilePath()
	{
		// TODO: Make this more easily configurable
		var url = "https://code.google.com/p/csant/downloads/list";

		var xpath = "//table[@id='resultstable']/tr/td[3]";

		var prefix = "csAnt-release-";

		var downloadBase = "https://csant.googlecode.com/files/";

		var data = ScrapeXPathArray(
			url,
			xpath
		);

		foreach (string item in data)
		{
			if (item.IndexOf(prefix) == 0)
			{
				return downloadBase + item;
			}
		}

		return String.Empty;
	}
}
