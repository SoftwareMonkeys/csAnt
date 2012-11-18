//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;
using System.Reflection;

class ImportFileNodesLibScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ImportFileNodesLibScript().Start(
			new Arguments(
				args
			)
		);
	}
	
	public void Start(Arguments args)
	{
		if (args.Contains("h"))
		{
			Help();
		}
		else
		{
			Execute(args);
		}
	}

	public void Execute(Arguments args)
	{
	
		string groupName = args["group"];

		if (!args.Contains("project"))
			throw new ArgumentException("A '-project:[name]' argument must be provided.");

		string projectName = args["project"];

		if (!args.Contains("release"))
			throw new ArgumentException("A '-release:[release]' argument must be provided.");

		string releaseName = args["release"];
		
		Console.WriteLine ("Project name: " + projectName);

		Console.WriteLine ("Release: " + releaseName);

		if (!String.IsNullOrEmpty(groupName))
		{
			Console.WriteLine (
				"Group name: {0}",
			    (
					String.IsNullOrEmpty(groupName)
					? "[null]"
					: groupName
				)
			);

			ImportLocalWebApp(groupName, projectName, releaseName);
		}
		else
		{
			ImportLocalWebApp(projectName, releaseName);
		}
	}

	public void Help()
	{
		Console.WriteLine("");
		Console.WriteLine("Required:");
		Console.WriteLine("");
		Console.WriteLine("-project:[project]  - The name of the web application project to import.");
		Console.WriteLine("");
		Console.WriteLine("-release:[release]  - The name of the release file to import.");
		Console.WriteLine("");

		Console.WriteLine("");
		Console.WriteLine("Optional:");
		Console.WriteLine("");
		Console.WriteLine("-group:[group]  - The name of the group/company that the project is part of. If this argument is not provided then the group of the destination project is used.");
		Console.WriteLine("");
	}
}
