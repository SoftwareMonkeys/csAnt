//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class CreateProjectNodeScript
{
	public static void Main(string[] args)
	{
		new CreateProjectNodeScript().Start(args);
	}
	
	public void Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Creating project node...");
		Console.WriteLine("");

		var projectName = Path.GetFileName(Environment.CurrentDirectory);

		if (args.Length > 0)
			projectName = args[0];

		Console.WriteLine("Project name: " + projectName);

		var filePath = Environment.CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ projectName
			+ ".node";

		if (!File.Exists(filePath))
		{
			var content = GetContent(projectName);

			Console.WriteLine("Node file path:");

			Console.WriteLine(filePath);

			File.WriteAllText(filePath, content);
		}
		else
			Console.WriteLine("Project node already found. Skipping creation.");
	}

	public string GetContent(string projectName)
	{
		return
@"{
  ""Name"": """ + projectName + @""",
  ""Properties"":{
    ""Context"": ""Project""
  }
}";

	}
}
