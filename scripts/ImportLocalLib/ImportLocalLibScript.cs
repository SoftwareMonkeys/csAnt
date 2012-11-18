//css_ref ../../lib/csAnt/SoftwareMonkeys.csAnt.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Reflection;

class ImportFileNodesLibScript : BaseScript
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
		string groupName = args["group"];

		if (!args.Contains("project"))
			throw new ArgumentException("A '-project:[name]' argument must be provided.");

		string projectName = args["project"];
		
		if (!args.Contains("mode"))
			throw new ArgumentException("A '-mode:[mode]' argument must be provided.");

		string buildMode = args["mode"];
		
		Console.WriteLine ("Project name: " + projectName);

		Console.WriteLine ("Build mode: " + buildMode);

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

			ImportLocalLib(groupName, projectName, buildMode);
		}
		else
		{
			ImportLocalLib(projectName, buildMode);
		}
	}
}
