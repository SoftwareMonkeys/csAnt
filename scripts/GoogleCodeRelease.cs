//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class GoogleCodeReleaseScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new GoogleCodeReleaseScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Uploading the setup and project release files to Google Code...");
		Console.WriteLine("");
	
		UploadSetupFiles();
		//UploadReleaseFiles();

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


	public void UploadSetupFiles()
	{
		var binDir = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "bin"
			+ Path.DirectorySeparatorChar
			+ "Release"
			+ Path.DirectorySeparatorChar
			+ "packed";

		var files = new string[]{
			"csAnt.exe",
			"csAnt-SetUp.exe"
		};

		foreach (string file in FindFiles(binDir, files))
		{
			UploadSetupFile(
				file
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

	public void UploadSetupFile(string file)
	{

		string targetName = Path.GetFileNameWithoutExtension(file)
			+ "--" + CurrentNode.Properties["Version"].Replace(".", "-")
			+ "-[" + TimeStamp + "]"
			+ Path.GetExtension(file);

		var tmpFilePath = GetTmpDir()
			+ Path.DirectorySeparatorChar
			+ targetName;

		File.Copy(file, tmpFilePath);

		GoogleCodeUpload(
			ProjectName,
			tmpFilePath,
			targetName
		);

		File.Delete(tmpFilePath);
	}
}
