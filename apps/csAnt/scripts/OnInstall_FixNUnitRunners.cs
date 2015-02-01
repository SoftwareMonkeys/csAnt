using System;
using System.IO;
using SoftwareMonkeys.csAnt;

class OnInstall_FixNUnitRunners : BaseScript
{
	public static void Main(string[] args)
	{
		new OnInstall_FixNUnitRunners().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		ExecuteScript("FixNUnitRunners");

		return !IsError;
	}
}
