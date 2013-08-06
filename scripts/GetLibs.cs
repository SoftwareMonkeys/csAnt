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
		new GetLibsScript().Start(new Arguments(args));
	}
	
	public override bool Start(string[] args)
	{
		var force = false;

		if (args.Contains("force"))
			force = Convert.ToBoolean(args["force"]);

		GetLibs(force);

		return !IsError;
	}
}
