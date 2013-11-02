//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

[ExpectedArgument("name")]
[ExpectedArgument("path")]
class AddImportScript : BaseScript
{
	public static void Main(string[] args)
	{
		new AddImportScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		if (args.Length < 2)
			Console.WriteLine("Two arguments expected; name and path.");
		else
		{
			string name = args[0];

			string path = args[1];

			AddImport(name, url, subPath);
		}

		return !IsError;
	}
}
