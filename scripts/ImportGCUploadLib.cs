//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Reflection;

class ImportGCUploadLib : BaseScript
{

	public static void Main(string[] args)
	{
		new ImportGCUploadLib().Start(args);
	}
	
	public void Start(string[] args)
	{
		var libPath = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "lib";

		// GoogleCode Upload
		var gcUploadLibDir = libPath + Path.DirectorySeparatorChar + "GCUpload";
		DownloadAndUnzip(
			"http://gcupload.googlecode.com/files/GCUpload.zip",
			gcUploadLibDir
		);
	}


}
