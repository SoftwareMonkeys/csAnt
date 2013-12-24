//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class NugetInstallScript : BaseScript
{
	public static void Main(string[] args)
	{
		new NugetInstallScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var name = args[0];

		StartProcess("mono", "lib/nuget.exe install " + name);

		return true;
	}
}
