//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class ImportFileScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ImportFileScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var projectName = args[0];

        var pattern = args[1];
        
        Importer.ImportFile(projectName, pattern);
                
		return !IsError;
	}
}
