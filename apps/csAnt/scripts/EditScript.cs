using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class EditScriptScript : BaseScript
{
	public static void Main(string[] args)
	{
		new EditScriptScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var program = args[0];

        var scriptName = args[1];

        var path = new ScriptFileNamer().GetScriptPath(scriptName);

        Console.WriteLine("Opening script for editing...");
        Console.WriteLine("Program: " + program);
        Console.WriteLine("Script name: " + scriptName);

        Console.WriteLine("Script path:");
        Console.WriteLine(path);

        StartProcess(program, path);

        Console.WriteLine("The script has been launched in your specified program.");
        Console.WriteLine("Edit the script and save it, then launch it from this terminal.");

		return !IsError;
	}
}
