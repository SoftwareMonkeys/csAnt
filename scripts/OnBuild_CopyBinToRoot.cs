//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class OnBuild_CopyBinToRootScript : BaseScript
{
	public static void Main(string[] args)
	{
		new OnBuild_CopyBinToRootScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		ExecuteScript("CopyBinToRoot");

		return !IsError;
	}
}
