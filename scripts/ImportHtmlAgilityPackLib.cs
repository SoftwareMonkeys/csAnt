//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Reflection;

class ImportHtmlAgilityPackLib : BaseScript
{

	public static void Main(string[] args)
	{
		new ImportHtmlAgilityPackLib().Start(args);
	}
	
	public void Start(string[] args)
	{
		var libPath = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "lib";

		// HtmlAgilityPack
		var htmlAgilityPackLibDir = libPath + Path.DirectorySeparatorChar + "HtmlAgilityPack";
		DownloadAndUnzip(
			"http://download-codeplex.sec.s-msft.com/Download/Release?ProjectName=htmlagilitypack&DownloadId=437941&FileTime=129893731308330000&Build=19612",
			htmlAgilityPackLibDir
		);
	}


}
