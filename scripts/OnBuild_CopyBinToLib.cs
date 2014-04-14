using System;
using System.IO;
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
