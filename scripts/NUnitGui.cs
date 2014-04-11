//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class NUnitGui : BaseScript
{
	public static void Main(string[] args)
	{
		new NUnitGui().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var file = "lib/NUnit.Runners.2.6.0.12051/tools/nunit.exe";

        StartNewProcess("mono", file);

		return !IsError;
	}
}
