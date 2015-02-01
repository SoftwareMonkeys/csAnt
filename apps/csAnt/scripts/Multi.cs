//css_ref "SoftwareMonkeys.csAnt.dll";
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class MultiScript : BaseScript
{
	public static void Main(string[] args)
	{
		new MultiScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Executing multiple scripts in a row...");
		Console.WriteLine("");

		Console.WriteLine("Scripts: " + String.Join(", ", args));

		foreach (var s in args)
		{
			if (ScriptExists(s))
				ExecuteScript(s);
			else
			{
				Error("Script not found: " + s);
			}
		}

		return true;
	}
}
