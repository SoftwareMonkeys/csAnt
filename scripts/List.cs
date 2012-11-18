//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;

class ListScript : BaseScript
{
	public static void Main(string[] args)
	{
		new ListScript().Start();
	}
	
	public void Start()
	{
		var scriptsDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "scripts";

		var scriptsList = new List<string>();

		foreach (string file in Directory.GetFiles(scriptsDir, "*.cs"))
		{
			scriptsList.Add(
				Path.GetFileNameWithoutExtension(
					file
				)
			);
		}

		foreach (string dir in Directory.GetDirectories(scriptsDir))
		{
			scriptsList.Add(
				Path.GetFileName(
					dir
				)
			);
		}

		scriptsList.Sort();

		ShowList(scriptsList.ToArray());
	}

	public void ShowList(string[] scripts)
	{
		Console.WriteLine("");
		Console.WriteLine("The following scripts are available:");
		Console.WriteLine("");

		foreach (string script in scripts)
		{
			Console.WriteLine(script);
		}

		Console.WriteLine("");
	}
}
