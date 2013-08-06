//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class AddLibScript : BaseScript
{
	public static void Main(string[] args)
	{
		new AddLibScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		if (args.Length < 2)
			Console.WriteLine("Two arguments expected; name and URL (to zip file).");
		else
		{
			string name = args[0];

			string url = args[1];

			string subPath = String.Empty;
			if (args.Length == 3)
				subPath = args[2];

			AddLib(name, url, subPath);
		}

		return !IsError;
	}
}
