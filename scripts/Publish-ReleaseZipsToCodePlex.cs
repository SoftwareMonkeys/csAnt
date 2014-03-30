//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class PublishReleaseZipsScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new PublishReleaseZipsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Uploading the project release zip files to Google Code...");
		Console.WriteLine("");
	
		UploadReleaseFiles();

		return !IsError;
	}

	public void UploadReleaseFiles()
	{
		var releaseDir = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "rls";

		foreach (string dir in Directory.GetDirectories(releaseDir))
		{
			string latestRelease = GetNewestFile(dir);

			UploadReleaseFile(
				latestRelease
			);
		}
	}

	public void UploadReleaseFile(string latestRelease)
	{
		string toPath = Path.GetFileName(latestRelease);

		GoogleCodeUpload(
			ProjectName,
			latestRelease,
			toPath
		);
	}
}
