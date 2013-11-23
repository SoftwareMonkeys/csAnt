//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

/// <summary>
/// Retrieves the library files from the specified project, using the information found in the '/lib/[Project]/[Project].node' file and unzips the files into '/lib/[Project]/'.
/// If a 'ImportScript' property is found it will be launched to perform the import.
/// If a 'LocalZipFile' property is found it will be retrieved and extracted.
/// If a 'Url' property is found it will be downloaded and extracted.
/// If multiple source properties are specified it will attempt to use the first (in the order specified above) and fall back to the next if it fails, for example if no import script is found, it will look for the local zip file, if that's not found it will look for the zip file at the specified URL.
/// </summary>
class GetLibScript : BaseScript
{
	public static void Main(string[] args)
	{
		new GetLibScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var name = args[0];

		var force = false;

		var parser = new Arguments(args);

		if (parser.Contains("force"))
			force = Convert.ToBoolean(parser["force"]);
			
	        if (parser.Contains("f"))
			force = Convert.ToBoolean(parser["f"]);

		GetLib(name, force);

		return !IsError;
	}
}
