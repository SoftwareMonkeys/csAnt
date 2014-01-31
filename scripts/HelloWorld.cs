//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Contracts.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

public class HelloWorldScript : BaseScript
{
	public static void Main(string[] args)
	{
		new HelloWorldScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Hello world!");
		Console.WriteLine("");

		AddSummary("Wrote the words 'Hello world!' to the console.");

		// Or
		/*var cmd = new HelloWorldCommand(this);

		ExecuteCommand(cmd);
		*/

		return !IsError;
	}
}
