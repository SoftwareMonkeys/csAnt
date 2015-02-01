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
	
	public override bool Run(string[] args)
	{
		var force = false;

		var arguments = new Arguments(args);

		if (arguments.Contains("force"))
			force = Convert.ToBoolean(arguments["force"]);

		GetLibs(force);

		return !IsError;
	}
}
