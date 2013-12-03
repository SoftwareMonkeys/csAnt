//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class OnBuild_CopyBinToLibScript : BaseScript
{
	public static void Main(string[] args)
	{
		new OnBuild_CopyBinToLibScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		ExecuteScript("CopyBinToLib");

		return !IsError;
	}
}
