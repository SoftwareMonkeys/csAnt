//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class OnBuildPackage_CopyPackagesToRepositoryScript : BaseScript
{
	public static void Main(string[] args)
	{
		new OnBuildPackage_CopyPackagesToRepositoryScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		ExecuteScript("CopyPackagesToRepository");

		return !IsError;
	}
}
