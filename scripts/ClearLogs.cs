using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class ClearLogsScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ClearLogsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        
		Console.WriteLine("");
		Console.WriteLine("Clearing logs...");
		Console.WriteLine("");

                var dir = CurrentDirectory
                        + Path.DirectorySeparatorChar
                        + "logs";
                        
                if (Directory.Exists(dir))
                        Directory.Delete(dir, true);
                else
                {
                        Console.WriteLine("Directory not found. Skipping deletion.");
                        
                        Console.WriteLine("");
                }

		return !IsError;
	}

}
