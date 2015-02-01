//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Lists all available scripts.
/// </summary>
class ListScript : BaseScript
{
	public static void Main(string[] args)
	{
		new ListScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var scriptsDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "scripts";

		var scriptsList = new SortedDictionary<string, string>();

		foreach (string file in Directory.GetFiles(scriptsDir, "*.cs"))
		{
			scriptsList.Add(
				Path.GetFileNameWithoutExtension(
					file
				),
				GetDescription(file)
			);
		}

		foreach (string dir in Directory.GetDirectories(scriptsDir))
		{
			scriptsList.Add(
				Path.GetFileName(
					dir
				),
				GetDescription(GetScriptFile(dir))
			);
		}

		ShowList(scriptsList);

		return !IsError;
	}

	public void ShowList(SortedDictionary<string, string> scripts)
	{
		Console.WriteLine("");
		Console.WriteLine("The following scripts are available:");
		Console.WriteLine("");

		foreach (string script in scripts.Keys)
		{
			Console.WriteLine(" - " + script);

			var description = scripts[script];

			if (!String.IsNullOrEmpty(description))
			{			
				Console.WriteLine("     " + description);
			}
			Console.WriteLine("");
		}

		Console.WriteLine("");
	}

	public string GetDescription(string scriptPath)
	{
		if (String.IsNullOrEmpty(scriptPath))
			return String.Empty;
		else
		{
			var content = File.ReadAllText(scriptPath);

			var pattern = @"<summary>(.*?)</summary>
class";

			var regex = new Regex(pattern, RegexOptions.Singleline);

			var match = regex.Match(content);

			var value = match.Groups[1].Value.Replace("///", "").Trim();

			return value;
		}
	}

	public string GetScriptFile(string dir)
	{
		var file = String.Empty;

		foreach (string f in Directory.GetFiles(dir, "*Script.cs"))
		{
			file = f;
		}

		// If there's no file ending with "Script" then grab any .cs file and try it
		if (String.IsNullOrEmpty(file))
		{
			foreach (string f in Directory.GetFiles(dir, "*.cs"))
			{
				file = f;
			}
		}

		return file;
	}
}
