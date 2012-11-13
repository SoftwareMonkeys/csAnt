//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;

class UpdateScript : BaseProjectScript
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
		
		Directory.Delete(tmpDir, true);
	}
	
	public void MoveToDestination(string tmpDir)
	{
		string subDir = Directory.GetDirectories(tmpDir)[0];

		var baseDir = ProjectDirectory;

		if (!Directory.Exists(baseDir))
			Directory.CreateDirectory(baseDir);
		
		Console.WriteLine ("");
		Console.WriteLine ("Updating files...");
		Console.WriteLine ("");

		foreach (string file in Directory.GetFiles (subDir, "*", SearchOption.AllDirectories))
		{
			var toFile = file.Replace(subDir, baseDir);

			if (!Directory.Exists(Path.GetDirectoryName(toFile)))
				Directory.CreateDirectory(Path.GetDirectoryName(toFile));

			var cmd = "Updating";

			// TODO: See if the following if statements an be better organised
			if (
				// If the file is newer than the existing one
				File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile)
			)
			{
				// If no to file already exists change the command text to "Adding".
				if (!File.Exists(toFile))
				{
					cmd = "Adding";
				}

				if (
					!File.Exists(toFile)
				    // If both files are different
					|| !FileEquals(file, toFile)
				)
				{
					Console.WriteLine (cmd + ":");
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

	}

	public string GetRemotecsAntFilePath()
	{
		// TODO: Make this more easily configurable
		var url = "https://code.google.com/p/csant/downloads/list";

		var xpath = "//table[@id='resultstable']/tr/td[3]";

		var prefix = "csAnt-";

		if (CurrentNode.Properties.Length > 0
			&& CurrentNode.Properties["Context"] == "Project")
		{
			prefix += "ProjectRelease-";
		}
		else
		{
			prefix += "StandardRelease-";
		}

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
