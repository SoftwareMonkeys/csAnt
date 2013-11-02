//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class ImportScriptScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ImportScriptScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Importing scripts...");
		Console.WriteLine("");

                var pattern = args[0];

		Console.WriteLine("");
                Console.WriteLine("Pattern:");
                Console.WriteLine(pattern);
		Console.WriteLine("");

                var csAntImportPath = ImportedDirectory
                        + Path.DirectorySeparatorChar
                        + "csAnt";

                var csAntScriptsPath = csAntImportPath
                        + Path.DirectorySeparatorChar
                        + "scripts";

                foreach (var f in Directory.GetFiles(csAntScriptsPath, pattern))
                        Console.WriteLine(f);

		/*AddSummary("Imported: " + scriptName);

                AddImport(
                        "csAnt",
                        "https://code.google.com/p/csant/"
                );*/

		return !IsError;
	}
}
