//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class UpdateScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new UpdateScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        ExecuteScript("AddCsAntImport");
	        
	        ImportFile("csAnt", "scripts/HelloWorld.cs");
	        ImportFile("csAnt", "scripts/Update.cs");
	        ImportFile("csAnt", "scripts/AddImport.cs");
	        ImportFile("csAnt", "scripts/GetCsAntLib.cs");
	        ImportFile("csAnt", "scripts/AddCsAntImport.cs");
	        ImportFile("csAnt", "scripts/ImportFile.cs");
	        ImportFile("csAnt", "scripts/ImportScript.cs");
	        ImportFile("csAnt", "scripts/ImportSync.cs");
	        ImportFile("csAnt", "scripts/ExportFile.cs");
	        ImportFile("csAnt", "scripts/GetLibs.cs");
	        ImportFile("csAnt", "scripts/InitSoftwareMonkeys.cs");
	        ImportFile("csAnt", "scripts/Initialize/*");
	        
	        ExecuteScript("GetCsAntLib", "-f");
	        
	        GetLib("csAnt", true);

		return !IsError;
	}
}
