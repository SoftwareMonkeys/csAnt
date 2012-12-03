//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class GetLibsScript : BaseScript
{
	public static void Main(string[] args)
	{
		new GetLibsScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		var force = false;

		var parser = new Arguments(args);

		if (parser.Contains("force"))
			force = Convert.ToBoolean(parser["force"]);

		GetLibs(force);

		return !IsError;
	}
}
