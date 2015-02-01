//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class ClearDllsScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ClearDllsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Clearing .bak files from project...");
		Console.WriteLine("");

		Clear();
		
		AddSummary("Cleared the project of .bak files.");

		return !IsError;
	}
	
	public void Clear()
	{
            foreach (var file in Directory.GetFiles(CurrentDirectory, "*.bak", SearchOption.AllDirectories))
            {
                Console.WriteLine(file);
                
                File.Delete(file);
            }	
        }
}
