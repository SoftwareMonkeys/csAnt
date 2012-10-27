//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Reflection;

class ImportLibs : BaseScript
{

	public static void Main(string[] args)
	{
		new ImportLibs().Start(args);
	}
	
	public void Start(string[] args)
	{
		ImportPrimaryLibs();

		ImportCustomLibs();
	}

	public void ImportPrimaryLibs()
	{
		// FileNodes
		ExecuteScript("ImportFileNodesLib");

		// TODO: Add other critical dependency imports
	}

	public void ImportCustomLibs()
	{
		if (ScriptExists("ImportCustomLibs"))
		{
			Console.WriteLine("");
			Console.WriteLine("'ImportCustomLibs' script found. Executing...");
			Console.WriteLine("");

			ExecuteScript("ImportCustomLibs");
		}
		else
		{
			Console.WriteLine("");
			Console.WriteLine("No 'ImportCustomLibs' script found. Skipping.");
			Console.WriteLine("");
		}
	}
}
