//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";

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
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Adding csAnt import...");
		Console.WriteLine("");

		AddSummary("Added csAnt import info");

        if (!Importer.ImportExists("csAnt"))
        {
                Importer.AddImport(
                        "csAnt",
                        "https://github.com/SoftwareMonkeys/csAnt.git" // TODO: Make this configurable
                );
        }

		return !IsError;
	}
}
