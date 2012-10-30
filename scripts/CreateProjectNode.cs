//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class CreateProjectNodeScript : BaseScript
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

		var projectName = "Example";

		if (args.Length > 0)
			projectName = args[0];

		Console.WriteLine("Project name: " + projectName);

		var content = GetContent(projectName);

		var filePath = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ projectName
			+ ".node";

		Console.WriteLine("Node file path:");

		Console.WriteLine(filePath);

		File.WriteAllText(filePath, content);
	}

	public string GetContent(string projectName)
	{
		return
@"{
  ""Context"": ""Project"",
  ""Name"": """ + projectName + @"""
}";

	}
}
