//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Scripting.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects.Scripting;

[ExpectedArgument("name")]
[ExpectedArgument("path")]
class AddImportScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new AddImportScript().Start(args);
	}
	
	public override bool Run(string[] args)
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
