//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";

using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

[ExpectedArgument]
class BuildSolutionScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new BuildSolutionScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("Looking for solution (*.sln) files...");
				
		if (args.Length > 0)
			BuildByShortName(args[0]);
		else
			ShowIndex();

		return true;
	}
	
	public void ShowIndex()
	{
		var rootPath = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "src";
	
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
	
		var rootPath = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "src";
	
		string pattern = String.Empty;

		if (shortName.IndexOf(".sln") > -1)
			pattern = "*";
		else
			pattern = "*.sln";

		var solutionFiles = Directory.GetFiles(rootPath, pattern, SearchOption.AllDirectories);
		
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
