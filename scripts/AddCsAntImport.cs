//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class AddCsAntImportScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new AddCsAntImportScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Adding csAnt import...");
		Console.WriteLine("");

		AddSummary("Added csAnt import info");

        if (!ImportExists("csAnt"))
        {
                AddImport(
                        "csAnt",
                        "https://code.google.com/p/csant/"
                );
        }

		return !IsError;
	}
}
