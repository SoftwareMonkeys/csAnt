//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class BuildSolutionScript : BaseScript
{
	public static void Main(string[] args)
	{
		new BuildSolutionScript().Start(args);
	}
	
	public void Start(string[] args)
	{
		Console.WriteLine("Looking for solution (*.sln) files...");
				
		if (args.Length > 0)
			BuildByShortName(args[0]);
		else
			ShowIndex();
	}
	
	public void ShowIndex()
	{
		var rootPath = CurrentDirectory;
	
	
		var solutionFiles = Directory.GetFiles(rootPath, "*.sln", SearchOption.AllDirectories);
		
		int i = 0;
		
		if (solutionFiles.Length == 1)
		{
			BuildSolution(solutionFiles[0]);
		}
		else
		{
			Console.WriteLine("Found solutions:");
			
			foreach (string solutionFile in solutionFiles)
			{
				i++; 
				
				var name = Path.GetFileNameWithoutExtension(solutionFile);
				
				Console.WriteLine(name + " - " + solutionFile);
			}
			
			Console.WriteLine("Please specify one of the solution names as a parameter (without the path or .sln extension).");
		}
	}
	
	public void BuildByShortName(string shortName)
	{
		Console.WriteLine("Building by short name: " + shortName);
	
		var rootPath = CurrentDirectory;
	
		var solutionFiles = Directory.GetFiles(rootPath, "*.sln", SearchOption.AllDirectories);
		
		bool foundSolution = false;
		
		foreach (string solutionFile in solutionFiles)
		{	
			if (!IsError)
			{
				if (!foundSolution)
					foundSolution = true;
		
				var fileName = Path.GetFileNameWithoutExtension(solutionFile);
				var ext = Path.GetExtension(solutionFile).Trim('.');
			
				if (
					fileName.ToLower() == shortName.ToLower()
					&& ext.ToLower() == "sln"
				)
				{			
					if (!BuildSolution(solutionFile))
						IsError = true;
				}
			}

			if (IsError)
				break;
		}
		
		if (!foundSolution)
			Console.WriteLine("No solution found with the name: " + shortName);
	}
}
