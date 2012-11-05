//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class RemoveLibScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RemoveLibScript().Start(args);
	}
	
	public void Start(string[] args)
	{
		if (args.Length < 1)
		{
			Console.WriteLine("One argument expected; name.");
		}
		else
		{
			string name = args[0];

			RemoveLib(name);
		}
	}
}
