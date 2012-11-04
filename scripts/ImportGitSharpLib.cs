//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Reflection;

class ImportGitSharpLib : BaseScript
{

	public static void Main(string[] args)
	{
		new ImportGitSharpLib().Start(args);
	}
	
	public void Start(string[] args)
	{
		var libPath = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "lib";

		// GitSharp
		var gitSharpLibDir = libPath + Path.DirectorySeparatorChar + "GitSharp";
		DownloadAndUnzip(
			"http://cloud.github.com/downloads/henon/GitSharp/GitSharp-0.3.0.056f5345c8835fabc1a8f90548a9c5b0abdf9a68-release-net-3.5.zip",
			gitSharpLibDir
		);
	}


}
