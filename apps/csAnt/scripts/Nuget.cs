//css_ref "SoftwareMonkeys.csAnt.External.Nuget.dll";

using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.External.Nuget;

class NugetScript : BaseScript
{
	public static void Main(string[] args)
	{
		new NugetScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		new NugetExecutor().Execute(args);

		return !IsError;
	}
}
