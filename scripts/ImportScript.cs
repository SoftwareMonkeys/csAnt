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
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Importing scripts...");
		Console.WriteLine("");

                var pattern = args[0];

		Console.WriteLine("");
                Console.WriteLine("Pattern:");
                Console.WriteLine(pattern);
		Console.WriteLine("");

                var csAntImportPath = ImportStagingDirectory
                        + Path.DirectorySeparatorChar
                        + "csAnt";

                var csAntScriptsPath = csAntImportPath
                        + Path.DirectorySeparatorChar
                        + "scripts";

		Console.WriteLine("");
                Console.WriteLine("csAnt scripts path:");
                Console.WriteLine(csAntScriptsPath);
		Console.WriteLine("");

                var i = 0;
                
                AddImport(
                        "csAnt",
                        "https://code.google.com/p/csant/"
                );
                
                if (!pattern.Contains(".cs"))
                        pattern = pattern + ".cs";
                
                var files = FindFiles(csAntScriptsPath, pattern);

                foreach (var f in files)
                {
                        i++;
                        
                        var toFile = f.Replace(csAntImportPath, CurrentDirectory);
                        
                        Console.WriteLine("To file: " + toFile);
                        
                        if (File.Exists(toFile))
                                BackupFile(toFile);
                                
                        File.Copy(f, toFile, true);
                }
                

		Console.WriteLine("Imported " + i + " files.");
                
		AddSummary("Imported " + i + " files.");

		return !IsError;
	}
}
