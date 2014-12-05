using System;
using SoftwareMonkeys.csAnt;

public class HelloAppScript : BaseScript
{
	public static void Main(string[] args)
	{
		new HelloAppScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Hello app!");
		Console.WriteLine("");

		AddSummary("Wrote the words 'Hello app!' to the console.");

		return !IsError;
	}
}
